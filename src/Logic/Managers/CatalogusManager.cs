using Core.DTOs;
using Core.Enum;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
namespace Logic.Managers
{
    public class CatalogusManager : ICatalogusManager
    {

        private readonly IProductDatabaseService _productDatabaseService;

        public CatalogusManager(IProductDatabaseService productDatabaseService)
        {

            _productDatabaseService = productDatabaseService;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _productDatabaseService.GetAllProducts();
            return products.Select(p => p.ToProductDTO()).ToList();

        }

        public async Task<bool> AddProduct(ProductDTO product)
        {
            //Product DTO map to Entity 
            ProductEntity p = product.ToProductEntity();

            return await _productDatabaseService.AddProduct(p);
        }

        public async Task<bool> RemoveProduct(ProductDTO product)
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

        public async Task<bool> EditProduct(ProductDTO product)
        {
            // dto to entity
            ProductEntity productEntity = product.ToProductEntity();
            return await _productDatabaseService.EditProduct(productEntity);
        }

        public async Task<ProductDTO?> GetProductById(int Id)
        {
            var products = await _productDatabaseService.SearchProductById(Id);
            if (products.Count > 0)
            {
                // convert to ProductDTO's
                var productsDTO = products.Select(p => p.ToProductDTO()).ToList();

                return productsDTO[0];
            }
            return null;

        }

        public async Task<List<ProductDTO>> SearchProductBySearchterm(string searchterm)
        {
            var products = await _productDatabaseService.SearchProductBySearchTerm(searchterm);

            return products.Select(p => p.ToProductDTO()).ToList();
        }

        public Task<List<CategoryDTO>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> SearchProduct(string? searchterm = null, SortMethods sortMethod = SortMethods.NameAscending)
        {
            throw new NotImplementedException();
        }

        //     List<ProductDTO> ICatalogusManager.SearchProduct(string? searchterm, SortMethods sortMethod, int pageNumber = 1,
        // int pageSize = 10)
        //     {
        //         throw new NotImplementedException();
        //     }

        bool ICatalogusManager.AddProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        bool ICatalogusManager.RemoveProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        bool ICatalogusManager.EditProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        ProductDTO? ICatalogusManager.GetProductById(int Id)
        {
            throw new NotImplementedException();
        }

        List<CategoryDTO> ICatalogusManager.GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public int TotalProducts()
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> SearchProduct(int pageNumber, int pageSize, string? searchterm = null, SortMethods sortMethod = SortMethods.NameAscending)
        {
            throw new NotImplementedException();
        }

        List<ProductDTO> ICatalogusManager.GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public int TotalProducts(string? searchterm = null)
        {
            throw new NotImplementedException();
        }
    }
}