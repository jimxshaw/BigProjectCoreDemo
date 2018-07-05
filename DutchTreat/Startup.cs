using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DutchTreat
{
  public class Startup
  {
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
      _config = config;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DutchContext>(config =>
      {
        config.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
      });

      services.AddAutoMapper();

      services.AddTransient<IMailService, NullMailService>();

      services.AddTransient<DutchSeeder>();

      services.AddScoped<IDutchRepository, DutchRepository>();

      services.AddMvc()
              .AddJsonOptions(option => option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
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
