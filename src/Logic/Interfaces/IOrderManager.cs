using Core.DTOs;
using Data.Models;
namespace Logic
.Interfaces;

public interface IOrderManager
{
    Task<OrderDTO?> GetOrderById(int id);
    Task<int> CreateOrderId(int customerId);
    Task<bool> PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId);
    Task<List<OrderDTO>> GetOrders();
    Task<bool> UpdateOrder(OrderDTO order);
    Task<List<OrderDTO>> GetOrdersByCustomerId(int customerId);

    Task<List<OrderItemDTO>> GetOrderItemsByOrderId(int orderId);
    Task<bool> UpdateOrderStatus(OrderDTO order, Core.DTOs.OrderStatusDTO orderStatus);


}