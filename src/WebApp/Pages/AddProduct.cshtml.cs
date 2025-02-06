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
        public required ProductDTO? Product { get; set; }
        [BindProperty]
        public required string PriceString { get; set; }

        public required List<CategoryDTO> Categories { get; set; }

        // Managers
        private readonly ICatalogusManager _catalogusManager;


        public AddProductModel(ICatalogusManager catalogusManager)
        {

            _catalogusManager = catalogusManager;

        }

        public void OnGet(int? id)
        {
            if (id != null)
            {
                // edit product 
                Product = _catalogusManager.GetProductById((int)id);
                var price = Product != null ? ((float)Product.Price / 100.00).ToString() : "0.00";
                price = price.Replace(",", ".");

                PriceString = price;


            }
            LoadCategories();
        }

        public IActionResult OnPost()
        {
            //save product
            if (ModelState.IsValid)
            {

                PriceString = PriceString.Replace(".", ",");

                int PriceStringToInt = (int)(float.Parse(PriceString) * 100);
                if (Product != null)
                {
                    Product.Price = PriceStringToInt;
                }

                bool ProductSaved;

                if (Product != null && Product.Id != null)
                {
                    //save edit
                    ProductSaved = _catalogusManager.EditProduct(Product);

                }
                else
                {
                    //save new product

                    ProductSaved = Product != null && _catalogusManager.AddProduct(Product);

                }

                if (ProductSaved)
                {
                    //back to index
                    return RedirectToPage("Index");
                }
                else
                {
                    LoadCategories();
                    //product not saved
                    ModelState.AddModelError("", "Fout bij opslaan van product. Probeer opnieuw.");
                    return Page();

                }

            }

            //modelstate not valid 
            LoadCategories();
            return Page();

        }


        private void LoadCategories()
        {
            Categories = _catalogusManager.GetAllCategories();

        }


    }
}
