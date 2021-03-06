﻿using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<DutchRepository> _logger;


    public DutchRepository(DutchContext context, ILogger<DutchRepository> logger)
    {
      _context = context;
      _logger = logger;
    }


    public IEnumerable<Order> GetOrders(bool includeItems)
    {
      try
      {
        _logger.LogInformation("Get orders was called...");

        if (includeItems)
        {
          return _context.Orders
                         .Include(o => o.Items)
                         .ThenInclude(i => i.Product)
                         .OrderBy(o => o.OrderNumber)
                         .ToList();
        }
        else
        {
          return _context.Orders
                         .OrderBy(o => o.OrderNumber)
                         .ToList();
        }


      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders: {ex}");

        return null;
      }
    }


    public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
    {
      try
      {
        _logger.LogInformation("Get orders was called...");

        if (includeItems)
        {
          return _context.Orders
                         .Where(o => o.User.UserName == username)
                         .Include(o => o.Items)
                         .ThenInclude(i => i.Product)
                         .OrderBy(o => o.OrderNumber)
                         .ToList();
        }
        else
        {
          return _context.Orders
                         .Where(o => o.User.UserName == username)
                         .OrderBy(o => o.OrderNumber)
                         .ToList();
        }


      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders: {ex}");

        return null;
      }
    }


    public Order GetOrderById(string username, int id)
    {
      try
      {
        _logger.LogInformation("Get order by id was called...");

        return _context.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .Where(o => o.Id == id && o.User.UserName == username)
                       .FirstOrDefault();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get order by id: {ex}");

        return null;
      }
    }


    public IEnumerable<Product> GetProducts()
    {
      try
      {
        _logger.LogInformation("Get products was called...");

        return _context.Products
                       .OrderBy(p => p.Title)
                       .ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get products: {ex}");

        return null;
      }


    }


    public IEnumerable<Product> GetProductsByCategory(string category)
    {
      try
      {
        _logger.LogInformation("Get products by category was called...");

        return _context.Products
                             .Where(p => p.Category == category)
                             .OrderBy(p => p.Title)
                             .ToList();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get products by category: {ex}");

        return null;
      }


    }


    public void AddEntity(object model)
    {
      _context.Add(model);
    }


    public bool SaveAll()
    {
      // The number of rows returned after a successful save
      // should be greater than zero.
      return _context.SaveChanges() > 0;
    }


  }
}
