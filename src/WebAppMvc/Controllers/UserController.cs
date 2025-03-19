
using System.Security.Claims;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace WebAppMvc.Controllers;

public class UserController : Controller
{
    private readonly ILoginManager _loginManager;

    public UserController(ILoginManager loginManager)
    {
        _loginManager = loginManager;
    }

    public IActionResult Login(string? returnUrl)
    {
        return View();
    }



    [HttpPost, ActionName("Login")]
    public async Task<IActionResult> LoginAttempt(string username, string? returnUrl = null)
    {
        // Probeert de gebruiker in te loggen op basis van de opgegeven gebruikersnaam
        var user = _loginManager.UserLogin(username);

        // Als de gebruiker gevonden is (d.w.z. de login was succesvol)
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

            // Als de login succesvol was, sturen we de gebruiker door naar de "Index" actie van de "Home" controller
            return RedirectToAction("Index", "Products");
        }

        // Als de login niet succesvol was (d.w.z. de gebruiker is niet gevonden), voegen we een foutmelding toe
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");

        // En we tonen de login pagina opnieuw met de foutmelding
        return View();
    }

    public async Task<IActionResult> LogoutAsync()
    {
        // Log de gebruiker uit door de authenticatie-cookie te verwijderen
        await HttpContext.SignOutAsync("MyCookieAuth");
        //Logout
        return RedirectToAction("Index", "Home");

    }
}
