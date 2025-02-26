using System.Text.Json;
using System.Text.Json.Nodes;
using Core.DTOs;
using Core.Enum;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Services;

namespace WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogusManager _catalogusManager;
    private readonly ISessionService _sessionService;




    [BindProperty]
    public string SearchTerm { get; set; } = "";

    public List<string> SortMethodsString { get; set; } = new List<string>();

    [BindProperty]
    public string SelectedSortMethod { get; set; }

    private int Pagesize = 9;
    [BindProperty]
    public int Totalpages { get; set; }
    [BindProperty]
    public int CurrentPage { get; set; }


    public UserDTO? LoggedInUser;

    public required List<ProductDTO> Producten { get; set; }

    public IndexModel(ICatalogusManager catalogusManager, ISessionService sessionService)
    {
        _catalogusManager = catalogusManager;
        _sessionService = sessionService;

        SortMethodsString = new List<string>
        {
            "Alfabetisch oplopend",
            "Alfabetisch aflopend",
            "Prijs oplopend",
            "Prijs aflopend"
        };

        SelectedSortMethod = SortMethodsString[0];
    }

    public void OnGet(int? pageNumber, [FromQuery] string? sortmethod, [FromQuery] string? search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            SearchTerm = search;
        }
        if (!string.IsNullOrEmpty(sortmethod))
        {
            SelectedSortMethod = sortmethod;
        }
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
        LoggedInUser = _sessionService.GetLoggedInUser(HttpContext);

        var sortMethod = SelectedSortMethod switch
        {
            "Alfabetisch oplopend" => SortMethods.NameAscending,
            "Alfabetisch aflopend" => SortMethods.NameDescending,
            "Prijs oplopend" => SortMethods.PriceAscending,
            "Prijs aflopend" => SortMethods.PriceDescending,
            _ => SortMethods.NameAscending
        };


        var totalProducts = _catalogusManager.TotalProducts(SearchTerm);
        Totalpages = (int)Math.Ceiling(totalProducts / (double)Pagesize);

        CurrentPage = page < 1 ? 1 : (page > Totalpages ? Totalpages : page);


        Producten = _catalogusManager.SearchProduct(CurrentPage, Pagesize, SearchTerm, sortMethod);

    }
}