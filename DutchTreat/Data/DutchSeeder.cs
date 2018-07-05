using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  public class DutchSeeder
  {
    private readonly DutchContext _context;
    private readonly IHostingEnvironment _hosting;
    private readonly UserManager<StoreUser> _userManager;

    public DutchSeeder(DutchContext context,
                       IHostingEnvironment hosting,
                       UserManager<StoreUser> userManager)
    {
      _context = context;
      _hosting = hosting;
      _userManager = userManager;
    }


    public async Task Seed()
    {
      _context.Database.EnsureCreated();

      var user = await _userManager.FindByEmailAsync("jimxshaw@gmail.com");

      if (user == null)
      {
        user = new StoreUser()
        {
          FirstName = "Jim",
          LastName = "Shaw",
          UserName = "jimxshaw@gmail.com",
          Email = "jimxshaw@gmail.com"
        };

        var result = await _userManager.CreateAsync(user, "P@ssword123!");

        if (result != IdentityResult.Success)
        {
          throw new InvalidOperationException("Failed to create default user");
        }

      }

      if (!_context.Products.Any())
      {
        // Need to create sample data.
        var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");

        var json = File.ReadAllText(filePath);

        var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

        _context.Products.AddRange(products);

        var order = new Order()
        {
          OrderDate = DateTime.Now,
          OrderNumber = "10001",
          User = user,
          Items = new List<OrderItem>()
          {
            new OrderItem()
            {
              Product = products.First(),
              Quantity = 5,
              UnitPrice = products.First().Price
            }

          }
        };

        _context.Orders.Add(order);

        _context.SaveChanges();
      }

    }


  }
}
