using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DutchTreat
{
  public class Startup
  {
    private readonly IConfiguration _config;
    private readonly IHostingEnvironment _environment;

    public Startup(IConfiguration config,
                   IHostingEnvironment environment)
    {
      _config = config;
      _environment = environment;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddIdentity<StoreUser, IdentityRole>(config =>
      {
        config.User.RequireUniqueEmail = true;
      }).AddEntityFrameworkStores<DutchContext>();

      services.AddDbContext<DutchContext>(config =>
      {
        config.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
      });

      services.AddAutoMapper();

      services.AddTransient<IMailService, NullMailService>();

      services.AddTransient<DutchSeeder>();

      services.AddScoped<IDutchRepository, DutchRepository>();

      services.AddMvc(options =>
      {
        if (_environment.IsProduction())
        {
          options.Filters.Add(new RequireHttpsAttribute());
        }
      }).AddJsonOptions(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
                          IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/error");
      }

      app.UseStaticFiles();

      // Turn on authentication after Identity has been
      // configured in the configure services method.
      // Ensure authentication is before mvc so that mvc
      // utilizes auth features. 
      app.UseAuthentication();

      app.UseMvc(config =>
      {
        config.MapRoute("Default",
          "{controller}/{action}/{id?}",
          new { controller = "App", Action = "index" });
      });

      if (env.IsDevelopment())
      {
        // Seed the database. 
        using (var scope = app.ApplicationServices.CreateScope())
        {
          var seeder = scope.ServiceProvider.GetService<DutchSeeder>();

          // Since Seed is an async method, we simply wait
          // for it to complete before proceeding.
          seeder.Seed().Wait();
        }
      }


    }
  }
}
