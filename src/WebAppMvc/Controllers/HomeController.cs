using System.Diagnostics;
using System.Security.Claims;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebAppMvc.Models;
using WebAppMvc.Services;

namespace WebAppMvc.Controllers;

public class HomeController : Controller
{

    private readonly ILoginManager _loginManager;


    public HomeController(ILoginManager loginManager)
    {

        _loginManager = loginManager;


    }

    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            // De gebruiker is ingelogd, je kunt hun gegevens gebruiken, bijvoorbeeld:
            var username = User.Identity.Name;
            return RedirectToAction("Index", "Products");
        }
        else
        {
            return RedirectToAction("Login", "User");
        }


    }




    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
