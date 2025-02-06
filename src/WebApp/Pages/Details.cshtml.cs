using System.Text.Json;
using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICatalogusManager _catalogusManager;

        [BindProperty]
        public ProductDTO? Product { get; set; }

        public UserDTO? LoggedInUser;


        public DetailsModel(ICatalogusManager catalogusManager)
        {
            _catalogusManager = catalogusManager;

        }
        public IActionResult OnGet(int id)
        {

            //get logged in user
            var userJson = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(userJson))
            {
                LoggedInUser = JsonSerializer.Deserialize<UserDTO>(userJson);


            }
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
