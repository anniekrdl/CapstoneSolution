using System.Data.Entity;
using Core.DTOs;
using Data.EF;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class CatalogusManagerEF : ICatalogusManager
{
    private readonly WebshopContext _webshopContext;
    public CatalogusManagerEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;

    }
    public Task<bool> AddProduct(ProductDTO product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> EditProduct(ProductDTO product)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductDTO>> GetAllProducts()
    {

        var products = _webshopContext.Products.ToList();
        // Nu kun je de Select() methode gebruiken op de productenlijst
        var productDtos = products.Select(p => p.ToProductDTO()).ToList();

        return productDtos;



    }

    public Task<ProductDTO?> GetProductById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveProduct(ProductDTO product)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductDTO>> SearchProductBySearchterm(string searchterm)
    {
        throw new NotImplementedException();
    }
}