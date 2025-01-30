using Core.DTOs;
using Data.EF;
using Logic.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.Services;
using Data.Interfaces;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Data.Models;
using WebApp.Managers;

namespace WebApp.Pages;

public class IndexModel : PageModel
{

    private readonly ICatalogusManager _catalogusManager;
    private readonly ILoginManager _loginManager;
    public readonly ISessionManager _sessionManager;

    [BindProperty]
    public string Username { get; set; }
    //Werkt niet met userDTO

    public UserDTO? LoggedInUser => _sessionManager.LoggedInUser;

    public List<CategoryDTO> Categories { get; set; }
    // required Hierdoor moet de property worden geïnitialiseerd, bijvoorbeeld via een object-initializer.
    public required List<ProductDTO> Producten { get; set; }




    public IndexModel(ICatalogusManager catalogusManager, ILoginManager loginManager, ISessionManager sessionManager)
    {
        _catalogusManager = catalogusManager;
        _loginManager = loginManager;
        _sessionManager = sessionManager;


    }

    public async Task OnGetAsync()
    {

        await LoadProducts();


    }

    public async Task<IActionResult> OnPostAsync()
    {

        //checks for annotation validation
        if (ModelState.IsValid)
        {

        }


        //Login
        var user = await _loginManager.UserLogin(Username);
        if (user != null)
        {
            _sessionManager.LoggedInUser = user;

            await LoadProducts();
            return Page();

        }
        return RedirectToPage();


    }

    private async Task LoadProducts()
    {
        var products = await _catalogusManager.GetAllProducts();

        Producten = products;

        Categories = await _catalogusManager.GetAllCategories();


    }
}
