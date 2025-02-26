using Core.DTOs;
using Core.Enum;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMvc.Models;
using WebAppMvc.Services;
using WebAppMvc.ViewModels;
namespace WebAppMvc.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly ICatalogusManager _catalogusManager;
    private readonly ILoginManager _loginManager;
    private readonly ICategoryManager _categoryManager;



    public ProductsController(ICatalogusManager catalogusManager, ILoginManager loginManager, ICategoryManager categoryManager)
    {
        _catalogusManager = catalogusManager;
        _loginManager = loginManager;
        _categoryManager = categoryManager;


    }

    public ActionResult Index(int? pageNumber, [FromQuery] string? sortmethod, [FromQuery] string? search)
    {

        IndexViewModel indexViewModel = new IndexViewModel();

        var userId = User.Identity.Name;
        indexViewModel.LoggedInUser = _loginManager.UserLogin(userId);

        if (!string.IsNullOrEmpty(search))
        {
            indexViewModel.SearchTerm = search;
        }
        if (!string.IsNullOrEmpty(sortmethod))
        {

            indexViewModel.SelectedSortMethod = sortmethod;
        }
        indexViewModel.CurrentPage = pageNumber ?? 1;

        var sortMethodEnum = Enum.Parse<SortMethods>(indexViewModel.SelectedSortMethod);

        var products = _catalogusManager.SearchProduct(indexViewModel.CurrentPage, indexViewModel.PageSize, indexViewModel.SearchTerm, sortMethodEnum);

        indexViewModel.Products = products;

        //total products
        var totalProducts = _catalogusManager.TotalProducts();
        indexViewModel.TotalPages = (int)Math.Ceiling(totalProducts / (double)indexViewModel.PageSize);



        //default view name of action. Dus Index hier
        return View(indexViewModel);
    }

    public IActionResult Details(int itemid)
    {


        var product = _catalogusManager.GetProductById(itemid);

        if (product == null)
        {
            return NotFound();
        }

        var userId = User.Identity.Name;

        DetailsViewModel detailsViewModel = new DetailsViewModel
        {
            productDTO = product,
            LoggedInUser = _loginManager.UserLogin(userId)
        };

        return View(detailsViewModel);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel productViewModel)
    {
        Console.WriteLine($"Save Product");

        if (!ModelState.IsValid)
        {
            var categories = _catalogusManager.GetAllCategories();
            productViewModel.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.Name,
                Text = c.Name

            }).ToList();

        }

        //Console.WriteLine(productViewModel.Product.CategoryId);
        //SaveProduct
        _catalogusManager.AddProduct(productViewModel.Product);


        return RedirectToAction("Index");
    }

    public IActionResult Create()
    {
        var categories = _catalogusManager.GetAllCategories();
        var categorySelectList = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

        ProductViewModel productViewModel = new ProductViewModel
        {
            Categories = categorySelectList
        };

        return View(productViewModel);
    }

    public IActionResult Edit(int id)
    {
        var categories = _catalogusManager.GetAllCategories();
        var categorySelectList = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

        var product = _catalogusManager.GetProductById(id);
        Console.WriteLine($"product is {product.Id} name {product.Name}");


        ProductViewModel productViewModel = new ProductViewModel
        {
            Product = product ?? new ProductDTO(),
            Categories = categorySelectList
        };

        Console.WriteLine($"in viewmodel product id is {productViewModel.Product.Id}");



        return View(productViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        //save product
        Console.WriteLine($"Edit product {productViewModel.Product.Stock} en id is {productViewModel.Product.Id}");

        _catalogusManager.EditProduct(productViewModel.Product);
        return RedirectToAction("Index");

    }

    public IActionResult Delete(int id)
    {
        //Delete product'
        var product = _catalogusManager.GetProductById(id);
        if (product != null)
        {
            _catalogusManager.RemoveProduct(product);
        }


        //Go back to index
        return RedirectToAction("Index");
    }




}