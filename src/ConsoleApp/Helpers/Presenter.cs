using Core.Models;
using Core.Interfaces;

namespace ConsoleApp.Helpers;

public class Presenter
{

    private readonly IOrderManager _orderManager;
    private readonly ICategoryManager _categoryManager;
    private readonly ICatalogusManager _catalogusManager;

    public Presenter(IOrderManager orderManager, ICategoryManager categoryManager, ICatalogusManager catalogusManager)
    {
        _orderManager = orderManager;
        _categoryManager = categoryManager;
        _catalogusManager = catalogusManager;

    }
    public void ShowShoppingCartItems(List<ShoppingCartItem> items)
    {
        double totalPrice = 0;
        Console.WriteLine(@"
            
        Productoverzicht:

         ID  | Productnaam         | Prijs        | Aantal      | TotaalPrijs
        --------------------------------------------------------");

        foreach (var item in items)
        {

            if (item.Product != null)
            {
                totalPrice += item.NumberOfItems * (item.Product.Price / 100.0);
                Console.WriteLine(
                    $"        {item.Id,-4} | {item.Product.Name,-20} | €{item.Product.Price / 100.0,-12:F2} | {item.NumberOfItems,-12} | €{item.NumberOfItems * (item.Product.Price / 100.0),-12:F2}"
                );
            }
        }

        Console.WriteLine(@$"
        TotaalPrijs winkelwagen: €{totalPrice,-12:F2}");
    }


    public async void ShowAllCategories()
    {
        List<Category> categories = await _categoryManager.GetCategories();
        Console.WriteLine(@"
            
        Categorieën:

         ID  | Categorienaam       | Beschrijving       
        ------------------------------------------------");

        foreach (var category in categories)
        {
            string description = category.Description.Length > 40
                ? category.Description.Substring(0, 40) + "..."
                : category.Description;

            Console.WriteLine($"{category.Id,-4} | {category.Name,-20} | {description}");
        }
    }

    public void ShowProduct(Product product)
    {
        Console.WriteLine(@"
            
        Productoverzicht:

         ID  | Productnaam         | Prijs       | Beschrijving
        --------------------------------------------------------");

        Console.WriteLine($"{product.Id,-4} | {product.Name,-20} | €{product.Price / 100.0,-12:F2} | {product.Description}");
    }

    public void ShowProducts(List<Product> products)
    {
        Console.WriteLine(@"
            
            Productoverzicht:

             ID  | Productnaam         | Prijs       | Beschikbaar
            --------------------------------------------------------");
        //List<Product> products = await GetAllProducts();
        foreach (Product p in products)
        {
            string productName = p.Name.PadRight(20);
            string productId = (p.Id.ToString() ?? "").PadRight(4);
            string productPrice = (p.Price / 100.00).ToString().PadRight(12);
            Console.WriteLine($@"             {productId}| {productName}| €{productPrice}| {p.Stock}");

        }

    }

    public async Task ShowOrders(List<Order> orders)
    {

        Console.WriteLine(@"
            Bestellingoverzicht:
            
            ID  | Datum         | Status       |
            -----------------------------------");

        foreach (Order order in orders)
        {
            string orderId = (order.Id ?? 0).ToString().PadRight(4);
            string date = (order.Date.ToString() ?? "").PadRight(14);
            string status = order.OrderStatus.ToString().PadRight(12);

            //orderItems

            List<OrderItem> orderItemsList = await _orderManager.GetOrderItemsByOrderId(order.Id ?? 0);



            Console.WriteLine($@"
            {orderId}| {date}| {status}
            ");
            foreach (OrderItem orderItem in orderItemsList)
            {
                //search product 
                Product? product = await _catalogusManager.GetProductById(orderItem.ProductId);
                //TODO product is null
                if (product != null)
                {
                    Console.WriteLine($@"               {orderItem.NumberOfItems,-4}x {product.Name,-24}      €{orderItem.NumberOfItems * (product.Price / 100.0),-12:F2}");

                }

            }

        }


    }




}