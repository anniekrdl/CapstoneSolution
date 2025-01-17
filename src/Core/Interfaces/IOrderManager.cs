using Core.Models;
namespace Core.Interfaces;

public interface IOrderManager
{
    Task<Order?> GetOrderById(int id);
    Task<int> CreateOrderId(int customerId);
    Task<bool> PlaceOrderFromShoppingCart(List<ShoppingCartItem> items, int? customerId);
    Task<List<Order>> GetOrders();
    Task<bool> UpdateOrder(Order order);
    Task<List<Order>> GetOrdersByCustomerId(int customerId);

    Task<List<OrderItem>> GetOrderItemsByOrderId(int orderId);
    Task<bool> UpdateOrderStatus(Order order, OrderStatus orderStatus);


}