using System;
using Core.Enum;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ICatalogusManager _catalogusManager;
    public ProductsController(ICatalogusManager catalogusManager)
    {
        _catalogusManager = catalogusManager;

    }

    // GET /api/products?searchTerm={searchTerm}&sortMethod={sortMethod}&pageNumber={pageNumber}&pageSize={pageSize}
    [HttpGet]
    public IActionResult GetProducts(
    [FromQuery] string? searchTerm,
    [FromQuery] SortMethods sortMethod,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10)
    {
        var products = _catalogusManager.SearchProduct(pageNumber, pageSize, searchTerm, sortMethod);
        //var products = _productService.GetProducts(searchTerm, sortMethod, pageNumber, pageSize);
        var totalProducts = _catalogusManager.TotalProducts();
        var response = new
        {
            Products = products,
            TotalProducts = totalProducts
        };
        return Ok(response);
    }

    //get product with id /api/product/details/{id}
    [HttpGet("{id}")]
    public IActionResult GetProductDetails(int id)
    {
        var product = _catalogusManager.GetProductById(id);

        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);

    }

    [HttpGet("categories")]
    public IActionResult GetAllCategories()
    {
        var categories = _catalogusManager.GetAllCategories();
        return Ok(categories);
    }

}
