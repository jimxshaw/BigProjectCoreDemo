using DutchTreat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
  public class DutchRepository : IDutchRepository
  {
    private readonly DutchContext _context;


    public DutchRepository(DutchContext context)
    {
      _context = context;
    }


    public IEnumerable<Product> GetProducts()
    {
      return _context.Products
                     .OrderBy(p => p.Title)
                     .ToList();
    }


    public IEnumerable<Product> GetProductsByCategory(string category)
    {
      return _context.Products
                     .Where(p => p.Category == category)
                     .OrderBy(p => p.Title)
                     .ToList();
    }


    public bool SaveAll()
    {
      // The number of rows returned after a successful save
      // should be greater than zero.
      return _context.SaveChanges() > 0;
    }


  }
}
