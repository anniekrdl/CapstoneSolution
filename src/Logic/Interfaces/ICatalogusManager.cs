using Core.DTOs;
using Data.Models;
namespace Logic
.Interfaces;

public interface ICatalogusManager
{

    Task<List<ProductDTO>> GetAllProducts();
    Task<bool> AddProduct(ProductDTO product);
    Task<bool> RemoveProduct(ProductDTO product);
    Task<bool> EditProduct(ProductDTO product);
    Task<ProductDTO?> GetProductById(int Id);
    Task<List<ProductDTO>> SearchProductBySearchterm(string searchterm);


}