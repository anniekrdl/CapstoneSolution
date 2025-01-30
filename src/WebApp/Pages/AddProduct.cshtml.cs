using System.Data.Entity;
using System.Threading.Tasks;
using Core.DTOs;
using Data.EF;
using Logic.Interfaces;
using Logic.Managers;
using Logic.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public required ProductDTO Product { get; set; }
        [BindProperty]
        public string PriceString { get; set; }

        public required List<CategoryDTO> Categories { get; set; }

        // Managers
        private readonly ICatalogusManager _catalogusManager;


        public AddProductModel(ICatalogusManager catalogusManager)
        {

            _catalogusManager = catalogusManager;

        }

        public async Task OnGet()
        {
            await LoadCategories();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //save product
            if (ModelState.IsValid)
            {

                Console.WriteLine($"Price string {PriceString}");
                Console.WriteLine($"to float {float.Parse(PriceString)} to decimal {decimal.Parse(PriceString)} x100 {float.Parse(PriceString) * 100} and to int: {(int)(float.Parse(PriceString) * 100)}");

                PriceString = PriceString.Replace(".", ",");

                int PriceStringToInt = (int)(float.Parse(PriceString) * 100);
                Product.Price = PriceStringToInt;


                var ProductSaved = await _catalogusManager.AddProduct(Product);

                if (ProductSaved)
                {
                    //back to index
                    return RedirectToPage("Index");
                }
                else
                {
                    await LoadCategories();
                    //product not saved
                    ModelState.AddModelError("", "Fout bij opslaan van product. Probeer opnieuw.");
                    return Page();

                }

            }

            //modelstate not valid 
            await LoadCategories();
            return Page();

        }


        private async Task LoadCategories()
        {
            Categories = await _catalogusManager.GetAllCategories();

        }


    }
}
