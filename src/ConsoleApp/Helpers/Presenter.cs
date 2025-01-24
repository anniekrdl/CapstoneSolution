using Logic.Interfaces;
using Core.DTOs;

namespace ConsoleApp.Helpers;

/// <summary>
/// Provides methods to present various data to the console.
/// </summary>
public class Presenter
{

    private readonly IOrderManager _orderManager;
    private readonly ICategoryManager _categoryManager;
    private readonly ICatalogusManager _catalogusManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="Presenter"/> class.
    /// </summary>
    /// <param name="orderManager">The order manager.</param>
    /// <param name="categoryManager">The category manager.</param>
    /// <param name="catalogusManager">The catalogus manager.</param>
    public Presenter(IOrderManager orderManager, ICategoryManager categoryManager, ICatalogusManager catalogusManager)
    {
        _orderManager = orderManager;
        _categoryManager = categoryManager;
        _catalogusManager = catalogusManager;

    }

    /// <summary>
    /// Displays the shopping cart items.
    /// </summary>
    /// <param name="items">The list of shopping cart items.</param>
    public void ShowShoppingCartItems(List<ShoppingCartItemDTO> items)
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

    /// <summary>
    /// Displays all categories.
    /// </summary>
    public async void ShowAllCategories()
    {
        List<CategoryDTO> categories = await _categoryManager.GetCategories();
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

    /// <summary>
    /// Displays the details of a product.
    /// </summary>
    /// <param name="product">The product to display.</param>
    public void ShowProduct(ProductDTO product)
    {
        Console.WriteLine(@"
            
        Productoverzicht:

         ID  | Productnaam         | Prijs       | Beschrijving
        --------------------------------------------------------");

        Console.WriteLine($"{product.Id,-4} | {product.Name,-20} | €{product.Price / 100.0,-12:F2} | {product.Description}");
    }

    /// <summary>
    /// Displays a list of products.
    /// </summary>
    /// <param name="products">The list of products to display.</param>
    public void ShowProducts(List<ProductDTO> products)
    {
        Console.WriteLine(@"
            
            Productoverzicht:

             ID  | Productnaam         | Prijs       | Beschikbaar
            --------------------------------------------------------");
        //List<Product> products = await GetAllProducts();
        foreach (ProductDTO p in products)
        {
            string productName = p.Name.PadRight(20);
            string productId = (p.Id.ToString() ?? "").PadRight(4);
            string productPrice = (p.Price / 100.00).ToString().PadRight(12);
            Console.WriteLine($@"             {productId}| {productName}| €{productPrice}| {p.Stock}");

        }

    }

    /// <summary>
    /// Displays a list of orders.
    /// </summary>
    /// <param name="orders">The list of orders to display.</param>
    public async Task ShowOrders(List<OrderDTO> orders)
    {

        Console.WriteLine(@"
            Bestellingoverzicht:
            
            ID  | Datum         | Status       |
            -----------------------------------");

        foreach (OrderDTO order in orders)
        {
            string orderId = (order.Id ?? 0).ToString().PadRight(4);
            string date = (order.Date.ToString() ?? "").PadRight(14);
            string status = order.OrderStatus.ToString().PadRight(12);

            //orderItems

            List<OrderItemDTO> orderItemsList = await _orderManager.GetOrderItemsByOrderId(order.Id ?? 0);



            Console.WriteLine($@"
            {orderId}| {date}| {status}
            ");
            foreach (OrderItemDTO orderItem in orderItemsList)
            {
                //search product 
                ProductDTO? product = await _catalogusManager.GetProductById(orderItem.ProductId);
                //TODO product is null
                if (product != null)
                {
                    Console.WriteLine($@"               {orderItem.NumberOfItems,-4}x {product.Name,-24}      €{orderItem.NumberOfItems * (product.Price / 100.0),-12:F2}");

                }

            }

        }


    }

}