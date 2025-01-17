using Core.Interfaces;
using Core.Models;
using Data.Services;
namespace Logic.Managers
{
    public class CatalogusManager : ICatalogusManager
    {

        private readonly IProductDatabaseService _productDatabaseService;

        public CatalogusManager(IProductDatabaseService productDatabaseService)
        {

            _productDatabaseService = productDatabaseService;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _productDatabaseService.GetAllProducts();
            return products;

        }

        public async Task<bool> AddProduct(Product product)
        {


            return await _productDatabaseService.AddProduct(product);
        }

        public async Task<bool> RemoveProduct(Product product)
        {
            //TODO
            if (product.Id != null)
            {
                return await _productDatabaseService.DeleteProduct((int)product.Id);
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> EditProduct(Product product)
        {
            return await _productDatabaseService.EditProduct(product);
        }

        public async Task<Product?> GetProductById(int Id)
        {
            var products = await _productDatabaseService.SearchProductById(Id);
            if (products.Count > 0)
            {
                return products[0];
            }
            return null;

        }

        public async Task<List<Product>> SearchProductBySearchterm(string searchterm)
        {
            var products = await _productDatabaseService.SearchProductBySearchTerm(searchterm);
            return products;
        }




    }
}