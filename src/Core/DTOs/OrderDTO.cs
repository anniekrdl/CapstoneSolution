namespace Core.DTOs;

public class OrderDTO
{
    public int? Id { get; set; }
    public int CustomerId { get; set; }
    public DateOnly? Date { get; set; }
    public OrderStatusDTO OrderStatus { get; set; }

    public void UpdateOrderStatus(OrderStatusDTO orderStatus)
    {
        OrderStatus = orderStatus;
    }
}

public enum OrderStatusDTO
{
    AANGEMAAKT,
    GEPLAATST,
    GEACCEPTEERD,
    GEWEIGERD,
    AFGEROND

}