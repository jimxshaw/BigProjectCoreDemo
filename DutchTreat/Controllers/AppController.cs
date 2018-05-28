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
  }
}
