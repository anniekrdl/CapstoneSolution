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

    public List<String> SortMethodsString { get; set; } = new List<string>();

    [BindProperty]
    public string SelectedSortMethod { get; set; }



    public UserDTO? LoggedInUser;


    // required Hierdoor moet de property worden geïnitialiseerd, bijvoorbeeld via een object-initializer.
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

    public void OnGet()
    {

        LoadProducts();
    }
    public IActionResult OnPost()
    {

        LoadProducts();
        return Page();
    }

    private void LoadProducts()
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

        var products = _catalogusManager.SearchProduct(searchterm: SearchTerm, sortMethod: sortMethod);
        Producten = products;
    }
}


