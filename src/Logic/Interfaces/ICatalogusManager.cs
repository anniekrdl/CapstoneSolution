using Core.DTOs;
using Core.Enum;
using Logic.Managers;
namespace Logic
.Interfaces;


public interface ICatalogusManager
{

    List<ProductDTO> GetAllProducts();

    int TotalProducts();
    bool AddProduct(ProductDTO product);
    bool RemoveProduct(ProductDTO product);
    bool EditProduct(ProductDTO product);
    ProductDTO? GetProductById(int Id);
    List<ProductDTO> SearchProduct(int pageNumber, int pageSize, string? searchterm = null, SortMethods sortMethod = SortMethods.NameAscending);
    List<CategoryDTO> GetAllCategories();


}