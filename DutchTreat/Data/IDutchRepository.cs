using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Order> GetOrders();

    Order GetOrderById(int id);

    IEnumerable<Product> GetProducts();

    IEnumerable<Product> GetProductsByCategory(string category);

    bool SaveAll();

  }
}