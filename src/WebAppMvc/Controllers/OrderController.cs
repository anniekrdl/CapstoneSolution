using Core.DTOs;
using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using WebAppMvc.ViewModels;
namespace WebAppMvc.Controllers;

[Authorize]
public class OrdersController : Controller
{

    private readonly IOrderManager _orderManager;
    private readonly ICatalogusManager _catalogusManager;

    public OrdersController(IOrderManager orderManager, ICatalogusManager catalogusManager)
    {
        _orderManager = orderManager;
        _catalogusManager = catalogusManager;
    }

    public ActionResult Index()
    {
        List<List<OrderForClient>> ordersByClient = new List<List<OrderForClient>>();

        OrderViewModel orderViewModel = new OrderViewModel();

        List<int> customerIds = _orderManager
        .GetOrders()
        .Select(o => o.CustomerId)
        .Distinct()
        .ToList();

        foreach (var customerId in customerIds)
        {
            var orderForClient = GetOrdersForClient(customerId);
            ordersByClient.Add(orderForClient);
        }

        orderViewModel.OrdersForClient = ordersByClient;

        return View(orderViewModel);

    }

    public void TestMethod()
    {
        OrderDTO order = _orderManager.GetOrderById(2);
        order.UpdateOrderStatus(OrderStatusDTO.GEACCEPTEERD);

        OrderDTO order1 = _orderManager.GetOrderById(1);

        Console.WriteLine($"orders {order.OrderStatus} nieuwe: {order1.OrderStatus}");

        //save order
        _orderManager.UpdateOrder(order);
    }

    public ActionResult Details(int itemId, string? SelectedStatus)
    {

        //TestMethod();

        if (!string.IsNullOrEmpty(SelectedStatus))
        {
            //update status
            OrderDTO order = _orderManager.GetOrderById(itemId);
            if (order != null)
            {

                order.UpdateOrderStatusFromString(SelectedStatus);
                _orderManager.UpdateOrder(order);

                order = _orderManager.GetOrderById(itemId);

                Console.WriteLine($"Order id is {order.Id} status na update is {order.OrderStatus}");

            }

        }

        OrderViewModel orderViewModel = new OrderViewModel();
        //get OrderForClient with itemId
        orderViewModel.OrderForClient = GetOrderForClient(itemId);

        return View(orderViewModel);

    }

    private OrderForClient? GetOrderForClient(int orderId)
    {

        Console.WriteLine($"orderID is {orderId}");

        var order = _orderManager.GetOrderById(orderId);

        if (order != null)
        {

            Console.WriteLine($"order not null id is {orderId} en status is na ophalen {order.OrderStatus}");

            var orderItems = _orderManager.GetOrderItemsByOrderId(orderId);

            float totalPrice = 0.0f;

            foreach (var item in orderItems)
            {
                var product = _catalogusManager.GetProductById(item.ProductId);
                item.Product = product;

                totalPrice += product.Price * item.NumberOfItems;
            }

            var orderForClient = new OrderForClient
            {
                CustomerId = order.CustomerId,
                OrderItems = orderItems,
                Order = order,
                TotalPrice = totalPrice
            };

            return orderForClient;

        }

        return null;

    }

    private List<OrderForClient> GetOrdersForClient(int CustomerId)
    {
        List<OrderForClient> orderForClients = new List<OrderForClient>();

        var orders = _orderManager.GetOrdersByCustomerId(CustomerId);
        foreach (var order in orders)
        {
            if (order.Id.HasValue)
            {
                var totalPrice = 0;
                var orderItems = _orderManager.GetOrderItemsByOrderId(order.Id.Value);

                foreach (var item in orderItems)
                {
                    var product = _catalogusManager.GetProductById(item.ProductId);
                    item.Product = product;

                    totalPrice += product.Price * item.NumberOfItems;
                }

                var orderForClient = new OrderForClient
                {
                    CustomerId = order.CustomerId,
                    OrderItems = orderItems,
                    Order = order,
                    TotalPrice = totalPrice

                };

                orderForClients.Add(orderForClient);

            }

        }

        return orderForClients;
    }

}

public class OrderForClient
{
    public int CustomerId { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
    public OrderDTO Order { get; set; }
    public float TotalPrice { get; set; }
}