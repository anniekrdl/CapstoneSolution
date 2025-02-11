using System.Text.Json;
using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICatalogusManager _catalogusManager;
        private readonly ISessionService _sessionService;

        [BindProperty]
        public ProductDTO? Product { get; set; }

        public UserDTO? LoggedInUser;


        public DetailsModel(ICatalogusManager catalogusManager, ISessionService sessionService)
        {
            _catalogusManager = catalogusManager;
            _sessionService = sessionService;

        }
        public IActionResult OnGet(int id, [FromQuery] string search)
        {
            Console.WriteLine($"Search term: {search}");

            //get logged in user
            LoggedInUser = _sessionService.GetLoggedInUser(HttpContext);


            Product = _catalogusManager.GetProductById(id);
            if (Product == null)
            {
                // Handle the case when the product is not found
                return NotFound();
            }

            return Page();



        }

        public IActionResult OnPost()
        {
            //Remove
            if (Product == null)
            {
                return NotFound();
            }
            _catalogusManager.RemoveProduct(Product);
            return RedirectToPage("Index");



        }
    }
}
