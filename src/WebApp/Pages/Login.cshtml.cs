using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Gebruikersnaam is verplicht.")]
        public required string UsernameString { get; set; }
        public string? ErrorMessage { get; set; }
        public void OnGet()
        {
            // ErrorMessage made when user not found in middleware
            ErrorMessage = HttpContext.Session.GetString("LoginError");
            HttpContext.Session.Remove("LoginError"); // Zorgt ervoor dat de melding maar één keer wordt getoond

        }

        public IActionResult OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                //Blijf op pagina maar toon wel de validatiefouten
                return Page();
            }

            HttpContext.Session.SetString("username", UsernameString);

            return RedirectToPage("Index");


        }
    }
}
