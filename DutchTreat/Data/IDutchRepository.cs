using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Order> GetOrders();

    IEnumerable<Product> GetProducts();

    IEnumerable<Product> GetProductsByCategory(string category);

    bool SaveAll();

  }
}