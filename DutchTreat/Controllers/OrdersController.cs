﻿using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  [Route("api/[Controller]")]
  public class OrdersController : Controller
  {
    private readonly IDutchRepository _repository;
    private readonly ILogger<OrdersController> _logger;
    private readonly IMapper _mapper;

    public OrdersController(IDutchRepository repository,
                            ILogger<OrdersController> logger,
                            IMapper mapper)
    {
      _repository = repository;
      _logger = logger;
      _mapper = mapper;
    }


    [HttpGet]
    public IActionResult Get()
    {
      try
      {
        return Ok(_repository.GetOrders());
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders: {ex}");

        return BadRequest("Failed to get orders");
      }
    }


    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
      try
      {
        var order = _repository.GetOrderById(id);

        if (order != null)
        {
          return Ok(_mapper.Map<Order, OrderViewModel>(order));
        }
        else
        {
          return NotFound();
        }

      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get orders: {ex}");

        return BadRequest("Failed to get orders");
      }
    }


    [HttpPost]
    public IActionResult Post([FromBody]OrderViewModel model)
    {
      try
      {
        if (ModelState.IsValid)
        {
          var newOrder = new Order()
          {
            Id = model.OrderId,
            OrderDate = model.OrderDate,
            OrderNumber = model.OrderNumber
          };

          if (newOrder.OrderDate == DateTime.MinValue)
          {
            newOrder.OrderDate = DateTime.Now;
          }

          _repository.AddEntity(newOrder);

          if (_repository.SaveAll())
          {
            var orderVM = new OrderViewModel()
            {
              OrderId = newOrder.Id,
              OrderDate = newOrder.OrderDate,
              OrderNumber = newOrder.OrderNumber
            };

            return Created($"/api/orders/{orderVM.OrderId}", orderVM);
          }
        }
        else
        {
          return BadRequest(ModelState);
        }

      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to save new order: {ex}");


      }

      return BadRequest("Failed to save new order");
    }




  }
}
