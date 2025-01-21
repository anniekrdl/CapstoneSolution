using System;
using Data.Models;

namespace Data





.Interfaces;

public interface IProductDatabaseService
{

    // get all products
    Task<List<ProductEntity>> GetAllProducts();

    // Add product to database
    Task<bool> AddProduct(ProductEntity product);

    // Edit product in database
    Task<bool> EditProduct(ProductEntity product);

    // Remove product from database
    Task<bool> DeleteProduct(int product_id);

    // Search product by id
    Task<List<ProductEntity>> SearchProductById(int Id);

    // search product by searchTerm
    Task<List<ProductEntity>> SearchProductBySearchTerm(string searchTerm);

}
