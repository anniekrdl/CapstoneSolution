
using ConsoleApp.Helpers;
using Core.DTOs;
using Logic.Interfaces;
namespace ConsoleApp.UI;

public class AdministratorMenu
{
    private readonly ICatalogusManager _catalogusManager;

    private readonly IOrderManager _orderManager;

    private bool _exitProgram = false;
    private readonly Presenter _presenter;

    public AdministratorMenu(ICatalogusManager catalogusManager, IOrderManager orderManager, Presenter presenter)
    {
        _catalogusManager = catalogusManager;
        _orderManager = orderManager;
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
            (3, "Bestellingen bekijken", new Action(() => ShowOrders().Wait())),
            (5, "Afsluiten", Exit),
        };

        MenuHelper.ShowMenu("AdminMenu", options);

        if (_exitProgram)
        {
            // exit programma
            return;
        }
    }

    private async Task ShowOrders()
    {
        List<OrderDTO> orders = await _orderManager.GetOrders();
        await _presenter.ShowOrders(orders);
        AdminOrderMenu();
    }

    private void AdminOrderMenu()
    {
        var options = new List<(int, string, Action)>
            {
                (1, "Accepteer een bestelling", new Action(() => UpdateOrder(OrderStatusDTO.GEACCEPTEERD).Wait())),
                (2, "Weiger een bestelling", new Action(() => UpdateOrder(OrderStatusDTO.GEWEIGERD).Wait())),
                (3, "Sluit een bestelling af", new Action(() => UpdateOrder(OrderStatusDTO.AFGEROND).Wait())),
                (4, "Terug naar hoofdmenu", () => { }),
            };

        MenuHelper.ShowMenu("Orderbeheer Menu", options);
    }

    private async Task UpdateOrder(OrderStatusDTO status)
    {
        int orderId = MenuHelper.GetUserInputInt($"Wat is de id van de bestelling? ");
        OrderDTO? order = await _orderManager.GetOrderById(orderId);

        if (order != null)
        {
            bool succes = await _orderManager.UpdateOrderStatus(order, status);
            if (succes)
            {
                Console.WriteLine(@$"
                De order is succesvol {status}
                ");
            }
            else
            {
                Console.WriteLine(@$"
                Het is niet mogelijk om een bestelling met status {order.OrderStatus} te updaten naar {status}
                ");
            }

        }
        else
        {
            Console.WriteLine($"Bestelling met id {orderId} is niet gevonden.");
        }
    }

    private void Exit()
    {
        _exitProgram = true;
    }

    private async Task SearchProduct()
    {
        string searchTerm = MenuHelper.GetUserInput("Wat is je zoekterm? ");
        //List<ProductDTO> productsFound = _catalogusManager.SearchProductBySearchterm(searchTerm);
        //_presenter.ShowProducts(productsFound);
    }

    private async Task ShowCatalog()
    {
        List<ProductDTO> products = _catalogusManager.GetAllProducts();
        _presenter.ShowProducts(products);
        //show catalog options
        AdminCatalogMenu();
    }

    private void AdminCatalogMenu()
    {
        bool backToMain = false;
        while (!backToMain)
        {
            var options = new List<(int, string, Action)>
        {
            (1, "Voeg product toevoegen", new Action(() => AddProductToCatalogus().Wait())),
            (2, "Een product verwijderen",  new Action(() => DeleteProduct().Wait())),
            (3, "Een product aanpassen", new Action(() => EditProduct().Wait())),
            (4, "Terug naar hoofdmenu", () => backToMain = true),
        };

            MenuHelper.ShowMenu("Catalogus Menu", options);

        }

    }

    private async Task EditProduct()
    {
        int id = MenuHelper.GetUserInputInt("Wat is de id van het product dat u wilt bewerken? ");
        ProductDTO? product = _catalogusManager.GetProductById(id);
        if (product != null)
        {
            ProductDTO updatedProduct = EditProductMenu(product);
            _catalogusManager.EditProduct(updatedProduct);
        }
    }

    private async Task DeleteProduct()
    {
        int id = MenuHelper.GetUserInputInt("Wat is de id van het product dat u wilt verwijderen? ");
        ProductDTO? p1 = _catalogusManager.GetProductById(id);
        if (p1 != null)
        {
            _catalogusManager.RemoveProduct(p1);
        }
        else
        {
            Console.WriteLine($"Geen product met id: {id} gevonden.");
        }
    }

    private async Task AddProductToCatalogus()
    {
        string name = MenuHelper.GetUserInput("De naam van het product: ");
        string description = MenuHelper.GetUserInput("De beschrijving van het product: ");
        int price = MenuHelper.GetUserInputInt("De prijs van het product in centen: ");
        int stock = MenuHelper.GetUserInputInt("Het aantal producten op voorraad: ");
        //Choose category
        _presenter.ShowAllCategories();
        int categoryId = MenuHelper.GetUserInputInt("De categorie-ID van het product: ");
        string imageUrl = MenuHelper.GetUserInput("De url voor de afbeelding van het product: ");
        ProductDTO p = new ProductDTO(null, name, description, price, stock, categoryId, imageUrl);
        _catalogusManager.AddProduct(p);
    }

    private ProductDTO EditProductMenu(ProductDTO product)
    {
        ProductDTO productCopy = new ProductDTO(product.Id, product.Name, product.Description, product.Price, product.Stock, product.CategoryId, product.ImageUrl);

        bool editing = true;
        while (editing)
        {
            var options = new List<(int, string, Action)>
                {
                    (1, "Naam wijzigen", () => productCopy.Name = MenuHelper.GetUserInput("Nieuwe naam: ")),
                    (2, "Beschrijving wijzigen", () => productCopy.Description = MenuHelper.GetUserInput("Nieuwe beschrijving: ")),
                    (3, "Prijs wijzigen", () => productCopy.Price = MenuHelper.GetUserInputInt("Nieuwe prijs (in centen): ")),
                    (4, "Voorraad wijzigen", () => productCopy.Stock = MenuHelper.GetUserInputInt("Nieuwe voorraad: ")),
                    (5, "Categorie wijzigen", () => {
                        _presenter.ShowAllCategories();
                        productCopy.CategoryId = MenuHelper.GetUserInputInt("Nieuwe categorie-ID: ");
                    }),
                    (6, "Afbeelding wijzigen", () => productCopy.ImageUrl = MenuHelper.GetUserInput("Nieuwe afbeeldings-URL: ")),
                    (7, "Stop met bewerken", () => editing = false),
                };

            MenuHelper.ShowMenu("Product Bewerken", options);
        }

        return productCopy;
    }


}