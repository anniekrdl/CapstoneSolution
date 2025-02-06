using Core.DTOs;
using Core.Enum;
using Logic.Managers;
namespace Logic
.Interfaces;


public interface ICatalogusManager
{

    Task<List<ProductDTO>> GetAllProducts();
    bool AddProduct(ProductDTO product);
    bool RemoveProduct(ProductDTO product);
    bool EditProduct(ProductDTO product);
    ProductDTO? GetProductById(int Id);
    List<ProductDTO> SearchProduct(string? searchterm = null, SortMethods sortMethod = SortMethods.NameAscending);
    List<CategoryDTO> GetAllCategories();


}