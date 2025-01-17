using Core.Interfaces;
using Core.Models;
using ConsoleApp.Helpers;
namespace ConsoleApp.UI;

public class CustomerMenu
{
    private readonly ICatalogusManager _catalogusManager;
    private readonly IShoppingCart _shoppingCart;
    private readonly IOrderManager _orderManager;
    private User _user;
    private bool _exitProgram = false;
    private readonly Presenter _presenter;


    public CustomerMenu(ICatalogusManager catalogusManager, IShoppingCart shoppingCart, IOrderManager orderManager, User user, Presenter presenter)
    {
        _catalogusManager = catalogusManager;
        _shoppingCart = shoppingCart;
        _orderManager = orderManager;
        _user = user;
        _presenter = presenter;
    }

    public bool Show()
    {
        while (!_exitProgram)
        {
            ShowMainMenu();
        }
        return !_exitProgram;
    }

    private void ShowMainMenu()
    {

        var options = new List<(int, string, Action)>
        {
            //ACTION kan alleen met void en niet async
            (1, "Bekijk catalogus",  new Action(() => ShowCatalog().Wait())),
            (2, "Zoek naar een product",new Action(() => SearchProduct().Wait())),
            (3, "Bekijk winkelwagen", new Action(() => ShowShoppingCart().Wait())),
            (4, "Bestelling bekijken", new Action(() => ShowOrders().Wait())),
            (5, "Afsluiten", Exit),
        };

        MenuHelper.ShowMenu("Klantmenu", options);

        if (_exitProgram)
        {
            // exit programma
            return;
        }
    }

    private async Task ShowOrders()
    {
        List<Order> orders = await _orderManager.GetOrdersByCustomerId((int)(_user.Id ?? 0));
        await _presenter.ShowOrders(orders);




    }


    private async Task ShowShoppingCart()
    {
        if (_user.Id != null)
        {
            List<ShoppingCartItem> items = await _shoppingCart.GetAllItemsByCustomerId((int)_user.Id, _catalogusManager);
            _presenter.ShowShoppingCartItems(items);
            CustomerShoppingCartMenu();
        }
    }

    private void CustomerShoppingCartMenu()
    {
        bool backToMain = false;

        while (!backToMain)
        {
            var options = new List<(int, string, Action)>{
            (1, "Alles is mijn winkelwagen bestellen", new Action(() => PlaceOrderFromShoppingCart().Wait())),
            (2, "Een item uit mijn winkelwagen verwijderen", new Action(() => RemoveProductFromShoppingCart().Wait())),
            (3, "Terug naar hoofdmenu", () => backToMain = true),
        };

            MenuHelper.ShowMenu("Winkelwagen menu", options);

        }

    }

    private async Task RemoveProductFromShoppingCart()
    {
        //remove product from shoppingCart
        int choice = MenuHelper.GetUserInputInt("Wat is de Id van het product dat je wilt verwijderen? ");
        //get item
        ShoppingCartItem? shoppingCartItem = await _shoppingCart.SearchById(choice, _catalogusManager);
        //Console.WriteLine($"items found: {shoppingCartItem.Count}");
        if (shoppingCartItem != null)
        {
            await _shoppingCart.RemoveShoppingCartItem(shoppingCartItem);
            if (shoppingCartItem.Product != null)
            {
                Console.WriteLine($"{shoppingCartItem.Product.Name} is verwijderd uit uw winkelwagen. ");
            }
            else
            {
                Console.WriteLine($"Het item met id {shoppingCartItem.Id} is verwijderd uit uw winkelwagen. ");
            }


        }
    }

    private async Task PlaceOrderFromShoppingCart()
    {
        List<ShoppingCartItem> items = await _shoppingCart.GetAllItemsByCustomerId((int)(_user.Id ?? 0), _catalogusManager);
        bool orderPlaced = await _orderManager.PlaceOrderFromShoppingCart(items, _user.Id);
        //empty shoppingcart
        if (orderPlaced)
        {
            await _shoppingCart.EmptyShoppingCart(items);

        }
    }

    private void Exit()
    {
        _exitProgram = true;
    }

    private async Task SearchProduct()
    {
        string searchTerm = MenuHelper.GetUserInput("Wat is je zoekterm? ");
        List<Product> productsFound = await _catalogusManager.SearchProductBySearchterm(searchTerm);

        _presenter.ShowProducts(productsFound);
    }

    private async Task ShowCatalog()
    {
        List<Product> products = await _catalogusManager.GetAllProducts();
        _presenter.ShowProducts(products);
        //show catalog options
        CustomerCatalogMenu();
    }

    private void CustomerCatalogMenu()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            var options = new List<(int, string, Action)>
        {
            (1, "Voeg product toe aan winkelwagen", new Action(() => AddProductToCart().Wait())),
            (2, "Bekijk product",  new Action(() => ViewProduct().Wait())),
            (3, "Terug naar hoofdmenu", () => backToMain = true),
        };

            MenuHelper.ShowMenu("Catalogus Menu", options);


        }


    }

    private async Task ViewProduct()
    {
        int Id = MenuHelper.GetUserInputInt("Wat is de Id van het product dat je wilt bekijken? ");
        Product? product = await _catalogusManager.GetProductById(Id);
        if (product != null)
        {
            _presenter.ShowProduct(product);
        }

    }

    private async Task AddProductToCart()
    {
        int productId = MenuHelper.GetUserInputInt("Wat is de id van het product dat je wilt toevoegen aan je winkelwagen? ");
        int numberOfItems = MenuHelper.GetUserInputInt("Hoeveel wilt u hier van toevoegen? ");

        //product to shoppingcartItem
        //TODO
        if (_user.Id != null)
        {
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem(null, (int)_user.Id, productId, null, numberOfItems);
            await _shoppingCart.AddShoppingCartItem(shoppingCartItem);
        }
    }
}