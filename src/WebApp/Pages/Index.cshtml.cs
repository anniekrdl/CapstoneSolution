using Core.DTOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Core.Enum;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogusManager _catalogusManager;
    private readonly ILoginManager _loginManager;

    [BindProperty]
    public required string Username { get; set; }

    [BindProperty]
    public string SearchTerm { get; set; } = "";

    public List<string> SortMethodsString { get; set; } = new List<string>();

    [BindProperty]
    public string SelectedSortMethod { get; set; }

    private int Pagesize = 5;
    [BindProperty]
    public int Totalpages { get; set; }
    [BindProperty]
    public int CurrentPage { get; set; }


    public UserDTO? LoggedInUser;

    public required List<ProductDTO> Producten { get; set; }

    public IndexModel(ICatalogusManager catalogusManager, ILoginManager loginManager)
    {
        _catalogusManager = catalogusManager;
        _loginManager = loginManager;

        SortMethodsString = new List<string>
        {
            "Alfabetisch oplopend",
            "Alfabetisch aflopend",
            "Prijs oplopend",
            "Prijs aflopend"
        };

        SelectedSortMethod = SortMethodsString[0];
    }

    public void OnGet(int? pageNumber)
    {
        Console.WriteLine($"pageNumber = {pageNumber}");

        CurrentPage = pageNumber ?? 1;

        LoadProducts(CurrentPage);
    }

    public IActionResult OnPost()
    {
        LoadProducts(CurrentPage);
        return Page();
    }

    private void LoadProducts(int page)
    {
        // initialize user
        var userJson = HttpContext.Session.GetString("user");

        if (!string.IsNullOrEmpty(userJson))
        {
            LoggedInUser = JsonSerializer.Deserialize<UserDTO>(userJson);
        }

        var sortMethod = SelectedSortMethod switch
        {
            "Alfabetisch oplopend" => SortMethods.NameAscending,
            "Alfabetisch aflopend" => SortMethods.NameDescending,
            "Prijs oplopend" => SortMethods.PriceAscending,
            "Prijs aflopend" => SortMethods.PriceDescending,
            _ => SortMethods.NameAscending
        };

        var totalProducts = _catalogusManager.TotalProducts();
        Totalpages = (int)Math.Ceiling(totalProducts / (double)Pagesize);

        CurrentPage = page < 1 ? 1 : (page > Totalpages ? Totalpages : page);


        Producten = _catalogusManager.SearchProduct(CurrentPage, Pagesize, SearchTerm, sortMethod);

    }
}