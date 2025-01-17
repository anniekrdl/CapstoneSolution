using Core.Models;
namespace Core.Interfaces;

public interface ICatalogusManager
{

    Task<List<Product>> GetAllProducts();
    Task<bool> AddProduct(Product product);
    Task<bool> RemoveProduct(Product product);
    Task<bool> EditProduct(Product product);
    Task<Product?> GetProductById(int Id);
    Task<List<Product>> SearchProductBySearchterm(string searchterm);


}