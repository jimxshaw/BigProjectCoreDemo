using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
  public interface IDutchRepository
  {
    IEnumerable<Order> GetOrders(bool includeItems);

    IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);

    Order GetOrderById(string username, int id);

    IEnumerable<Product> GetProducts();

    IEnumerable<Product> GetProductsByCategory(string category);

    void AddEntity(object model);

    bool SaveAll();

  }
}