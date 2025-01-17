using System;
using Core.Models;

namespace Core.Interfaces;

public interface IProductDatabaseService
{

    // get all products
    Task<List<Product>> GetAllProducts();

    // Add product to database
    Task<bool> AddProduct(Product product);

    // Edit product in database
    Task<bool> EditProduct(Product product);

    // Remove product from database
    Task<bool> DeleteProduct(int product_id);

    // Search product by id
    Task<List<Product>> SearchProductById(int Id);

    // search product by searchTerm
    Task<List<Product>> SearchProductBySearchTerm(string searchTerm);

}
