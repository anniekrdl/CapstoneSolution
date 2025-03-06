using System;
using System.Security.Claims;
using Core.DTOs;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly ILoginManager _loginManager;
    private readonly ICustomerManager _customerManager;



    public UserController(ILoginManager loginManager, ICustomerManager customerManager)
    {
        _loginManager = loginManager;
        _customerManager = customerManager;

    }

    [HttpGet]
    public IActionResult Login()
    {
        return Ok("Login");

    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginAttempt([FromBody] string username)
    {
        var user = _loginManager.UserLogin(username);

        if (user != null)
        {
            // Claims zijn stukjes informatie die we willen opslaan voor de gebruiker
            // In dit geval slaan we de gebruikersnaam en de unieke identifier (Id) op
            var claims = new List<Claim>
            {
            // De gebruikersnaam van de gebruiker (dit is de naam die in de login wordt gebruikt)
            new Claim(ClaimTypes.Name, user.UserName),
            
            // De unieke Id van de gebruiker (een numerieke identifier)
            new Claim(ClaimTypes.NameIdentifier, user.Id?.ToString() ?? string.Empty)
            };

            // ClaimsIdentity is een object dat de claims bevat en waarmee we de gebruiker kunnen identificeren
            // Het maakt de gebruiker 'identificeerbaar' in de app met een bepaalde identiteit
            var claimsIdentity = new ClaimsIdentity(claims, "login");

            // AuthenticationProperties bevatten instellingen voor het inloggen, zoals het instellen van de vervaltijd van de sessie
            var authProperties = new AuthenticationProperties
            {
                // Stelt in dat de login sessie 30 minuten geldig blijft
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),  // Verloopt na 30 minuten in UTC tijd

                // Stelt in dat het een 'persistente' sessie is, wat betekent dat de gebruiker ingelogd blijft, zelfs na het sluiten van de browser
                IsPersistent = true  // Bewaart de login voor meerdere sessies
            };

            // SignInAsync is de methode die de gebruiker daadwerkelijk inlogt
            // Het maakt gebruik van de 'MyCookieAuth' cookie-authenticatie schema om de gebruiker in te loggen
            // We geven een ClaimsPrincipal door (dat is de gebruiker met de claims) en de authentication properties
            await HttpContext.SignInAsync("MyCookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            // Als de login succesvol was, sturen een 200 Ok status code met de user info
            Console.WriteLine($"user is {user.GetType} user: {User.Identity}");

            return Ok(user);
        }

        // Als de login niet succesvol was (d.w.z. de gebruiker is niet gevonden), voegen we een foutmelding toe
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        // En we tonen de Ok pagina opnieuw met de foutmelding
        return Ok("user not found");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        // Log de gebruiker uit door de authenticatie-cookie te verwijderen
        await HttpContext.SignOutAsync("MyCookieAuth");
        //Logout
        return Ok("logged out");

    }

    [HttpPut("edit")]
    public IActionResult EditUser([FromBody] CustomerDTO changedUser)
    {
        if (changedUser == null)
        {
            return BadRequest("User data is null");
        }

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var username = User.Identity.Name;

            // change user
            bool customerChanged = _customerManager.EditCustomer(changedUser);

            if (customerChanged)
            {
                return Ok("Customer changed");

            }
            else
            {
                return BadRequest("Customer not changed");
            }

        }

        return Unauthorized();

    }

    [HttpPost("new")]
    public IActionResult AddCustomer([FromBody] CustomerDTO customer)
    {
        if (customer != null)
        {
            // add customer
            var customerAdded = _customerManager.AddCustomer(customer);
            if (customerAdded)
            {
                return Created();

            }
            else
            {
                return BadRequest("Customer not added");
            }

        }

        return BadRequest("Not a valid customer");

    }

    //as user you can only delete your own account
    [HttpDelete("delete")]
    public IActionResult DeleteCustomer()
    {
        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return BadRequest("User ID not found");
            }
            var userId = int.Parse(userIdClaim);

            var customerDeleted = _customerManager.RemoveCustomer(userId);

            if (customerDeleted)
            {
                return Ok("Customer Deleted");
            }
            else
            {
                return BadRequest("Customer not Deleted");
            }


        }
        else
        {
            return Unauthorized();
        }


    }
}
