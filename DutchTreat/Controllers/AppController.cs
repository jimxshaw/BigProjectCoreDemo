using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  public class AppController : Controller
  {

    private readonly IMailService _mailService;

    private readonly DutchContext _context;

    public AppController(IMailService mailService, DutchContext context)
    {
      _mailService = mailService;
      _context = context;
    }


    public IActionResult Index()
    {
      return View();
    }


    public IActionResult Contact()
    {
      return View();
    }


    [HttpPost]
    public IActionResult Contact(ContactViewModel model)
    {
      if (ModelState.IsValid)
      {
        // Send the email.
        _mailService.SendMessage("jimxshaw@gmail.com", model.Subject, $"Fromt: {model.Name} - {model.Email}, Message: {model.Message}");

        ViewBag.UserMessage = "Mail sent";

        ModelState.Clear();
      }
      else
      {
        // Show the errors.

      }

      return View();
    }


    public IActionResult About()
    {
      return View();
    }


    public IActionResult Shop()
    {
      var results = _context.Products
                            .OrderBy(p => p.Category)
                            .ToList();

      return View(results);
    }


  }
}
