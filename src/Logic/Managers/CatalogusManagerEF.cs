using Core.DTOs;
using Core.Enum;
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
    public bool AddProduct(ProductDTO product)
    {
        ProductEntity p = product.ToProductEntity();

        _webshopContext.Products.Add(p);

        int result = _webshopContext.SaveChanges();
        // als SaveChangesAsync() meer dan 0 rijen heeft toegevoegd, betekent dit dat de toevoeging succesvol was, en je kunt true retourneren.
        return result > 0;
    }

    public bool EditProduct(ProductDTO product)
    {
        try
        {

            Console.WriteLine($"product is {product.Stock}");
            Console.WriteLine($"product id is {product.Id}");

            // Entity instance are tracked when queried from database
            var existingProduct = _webshopContext.Products.Find(product.Id);
            if (existingProduct == null)
            {
                Console.WriteLine($"Product niet gevonden");
                // Product bestaat niet
                return false;
            }

            Console.WriteLine($"exisiting product: {existingProduct}");
            // Wijzig de velden van het bestaande product
            existingProduct.Name = product.Name ?? "";
            existingProduct.Description = product.Description ?? "";
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.ImageUrl = product.ImageUrl ?? "";

            // Sla de wijzigingen op in de database
            _webshopContext.SaveChanges();
            return true;

        }
        catch (System.Exception ex)
        {

            Console.WriteLine(ex.Message);
            return false;
        }

    }

    public List<CategoryDTO> GetAllCategories()
    {
        var categories = _webshopContext.Categories.ToList();
        return categories.Select(c => c.ToCategoryDTO()).ToList();
    }

    public List<ProductDTO> GetAllProducts()
    {
        var products = _webshopContext.Products.ToList();
        var productDtos = products.Select(p => p.ToProductDTO()).ToList();
        return productDtos;
    }

    public ProductDTO? GetProductById(int Id)
    {

        var product = _webshopContext.Products.FirstOrDefault(p => p.Id == Id);
        if (product == null)
        {
            return null;
        }

        return product.ToProductDTO();


    }

    public bool RemoveProduct(ProductDTO product)
    {
        try
        {
            var existingProduct = _webshopContext.Products.Find(product.Id);

            if (existingProduct == null)
            {
                return false;
            }

            _webshopContext.Remove(existingProduct);
            _webshopContext.SaveChanges();
            return true;


        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Product not deleted: {ex.Message}");
            return false;

        }
    }

    public List<ProductDTO> SearchProduct(int pageNumber,
    int pageSize, string? searchterm = null, SortMethods sortMethod = SortMethods.NameAscending)
    {
        // Check if search term exists
        bool isSearch = !string.IsNullOrWhiteSpace(searchterm);

        // Query as AsQueryable
        var query = _webshopContext.Products.AsQueryable();


        if (isSearch)
        {
            // If search term exists, search products
            query = query.Where(p => p.Name.Contains(searchterm!));
        }

        // Apply sorting
        query = sortMethod switch
        {
            SortMethods.NameAscending => query.OrderBy(p => p.Name),
            SortMethods.NameDescending => query.OrderByDescending(p => p.Name),
            SortMethods.PriceAscending => query.OrderBy(p => p.Price),
            SortMethods.PriceDescending => query.OrderByDescending(p => p.Price),
            _ => query
        };

        query = query
        .Skip((pageNumber - 1) * pageSize) // Skip de producten die al op vorige pagina's staan
        .Take(pageSize);// Neem alleen het aantal producten dat past op de huidige pagina

        // List of products
        var products = query.Select(p => p.ToProductDTO()).ToList();

        return products;
    }


    public int TotalProducts(string? searchterm = null)
    {
        var query = _webshopContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchterm))
        {
            query = query.Where(p => p.Name.Contains(searchterm));
        }

        return query.Count();
    }

}

