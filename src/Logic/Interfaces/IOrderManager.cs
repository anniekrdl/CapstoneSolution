using Core.DTOs;
namespace Logic
.Interfaces;

public interface IOrderManager
{
    OrderDTO? GetOrderById(int id);
    int CreateOrderId(int customerId);
    OrderDTO? PlaceOrderFromShoppingCart(List<ShoppingCartItemDTO> items, int? customerId);
    List<OrderDTO> GetOrders();
    bool UpdateOrder(OrderDTO order);
    List<OrderDTO> GetOrdersByCustomerId(int customerId);

    List<OrderItemDTO> GetOrderItemsByOrderId(int orderId);
    bool UpdateOrderStatus(OrderDTO order, Core.DTOs.OrderStatusDTO orderStatus);

}