using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
  public class AccountController : Controller
  {
    private readonly ILogger<AccountController> _logger;
    private readonly SignInManager<StoreUser> _signInManager;
    private readonly UserManager<StoreUser> _userManager;

    public AccountController(ILogger<AccountController> logger,
      SignInManager<StoreUser> signInManager,
      UserManager<StoreUser> userManager)
    {
      _logger = logger;
      _signInManager = signInManager;
      _userManager = userManager;
    }


    public IActionResult Login()
    {
      if (User.Identity.IsAuthenticated)
      {
        return RedirectToAction("Index", "App");
      }

      return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var result = await _signInManager.PasswordSignInAsync(model.Username,
                                                              model.Password,
                                                              model.RememberMe,
                                                              false);

        if (result.Succeeded)
        {
          if (Request.Query.Keys.Contains("ReturnUrl"))
          {
            Redirect(Request.Query["ReturnUrl"].First());
          }
          else
          {
            RedirectToAction("Shop", "App");
          }

        }

      }

      ModelState.AddModelError("", "Failed to login");

      return View();
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();

      return RedirectToAction("Index", "App");
    }


    [HttpPost]
    public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null)
        {
          var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

          if (result.Succeeded)
          {
            // Create the token.

          }
        }

      }

      return BadRequest();
    }
  }
}
