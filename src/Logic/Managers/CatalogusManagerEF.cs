using Core.DTOs;
using Data.EF;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
using Microsoft.EntityFrameworkCore; // Ensure this is the correct namespace

namespace Logic.Managers;

public class CatalogusManagerEF : ICatalogusManager
{

    private readonly WebshopContext _webshopContext;
    public CatalogusManagerEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;

    }
    public async Task<bool> AddProduct(ProductDTO product)
    {
        ProductEntity p = product.ToProductEntity();

        await _webshopContext.Products.AddAsync(p);

        int result = await _webshopContext.SaveChangesAsync();
        // als SaveChangesAsync() meer dan 0 rijen heeft toegevoegd, betekent dit dat de toevoeging succesvol was, en je kunt true retourneren.
        return result > 0;
    }

    public async Task<bool> EditProduct(ProductDTO product)
    {
        try
        {

            // Entity instance are tracked when queried from database
            var existingProduct = await _webshopContext.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                // Product bestaat niet
                return false;
            }
            // Wijzig de velden van het bestaande product
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ImageUrl = product.ImageUrl;

            // Sla de wijzigingen op in de database
            await _webshopContext.SaveChangesAsync();
            return true;

        }
        catch (System.Exception ex)
        {

            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public async Task<List<CategoryDTO>> GetAllCategories()
    {
        var categories = await _webshopContext.Categories.ToListAsync();
        return categories.Select(c => c.ToCategoryDTO()).ToList();
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {
        var products = await _webshopContext.Products.ToListAsync();
        var productDtos = products.Select(p => p.ToProductDTO()).ToList();
        return productDtos;
    }

    public async Task<ProductDTO?> GetProductById(int Id)
    {

        var product = await _webshopContext.Products.FirstOrDefaultAsync(p => p.Id == Id);
        if (product == null)
        {
            return null;
        }

        return product.ToProductDTO();


    }

    public async Task<bool> RemoveProduct(ProductDTO product)
    {
        try
        {
            var existingProduct = await _webshopContext.Products.FindAsync(product.Id);

            if (existingProduct == null)
            {
                return false;
            }

            _webshopContext.Remove(existingProduct);
            await _webshopContext.SaveChangesAsync();
            return true;


        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Product not deleted: {ex.Message}");
            return false;

        }
    }

    public Task<List<ProductDTO>> SearchProductBySearchterm(string searchterm)
    {
        var products = _webshopContext.Products
           .Where(p => p.Name.Contains(searchterm))
           .Select(p => p.ToProductDTO())
           .ToListAsync();

        return products;


    }
}