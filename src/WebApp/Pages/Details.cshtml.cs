using System.Threading.Tasks;
using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Managers;

namespace WebApp.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ICatalogusManager _catalogusManager;
        private readonly ISessionManager _sessionManager;

        [BindProperty]
        public ProductDTO? Product { get; set; }

        public UserDTO? LoggedInUser => _sessionManager.LoggedInUser;


        public DetailsModel(ICatalogusManager catalogusManager, ISessionManager sessionManager)
        {
            _catalogusManager = catalogusManager;
            _sessionManager = sessionManager;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {

            Product = await _catalogusManager.GetProductById(id);
            if (Product == null)
            {
                // Handle the case when the product is not found
                return NotFound();
            }

            return Page();



        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Remove
            if (Product == null)
            {
                return NotFound();
            }
            await _catalogusManager.RemoveProduct(Product);
            return RedirectToPage("Index");



        }
    }
}
