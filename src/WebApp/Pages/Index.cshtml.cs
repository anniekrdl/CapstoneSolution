using Core.DTOs;
using Data.EF;
using Logic.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Services;
using Data.Interfaces;
using Logic.Interfaces;

namespace WebApp.Pages;

public class IndexModel : PageModel
{

    ICatalogusManager _catalogusManager;
    public List<String> Producten { get; set; }



    public IndexModel(ICatalogusManager catalogusManager)
    {
        _catalogusManager = catalogusManager;


    }

    public async Task OnGetAsync()
    {
        await LoadProducts();


    }

    private async Task LoadProducts()
    {
        var products = await _catalogusManager.GetAllProducts();

        Producten = products.Select(x => x.Name).ToList();


    }
}
