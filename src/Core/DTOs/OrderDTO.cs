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

    public void UpdateOrderStatusFromString(string orderStatus)
    {
        var orderStatusDTO = FromStringToStatus(orderStatus);
        UpdateOrderStatus(orderStatusDTO);
    }

    public static OrderStatusDTO FromStringToStatus(string status)
    {
        return status.ToUpper() switch
        {
            "AANGEMAAKT" => OrderStatusDTO.AANGEMAAKT,
            "GEPLAATST" => OrderStatusDTO.GEPLAATST,
            "GEACCEPTEERD" => OrderStatusDTO.GEACCEPTEERD,
            "GEWEIGERD" => OrderStatusDTO.GEWEIGERD,
            "AFGEROND" => OrderStatusDTO.AFGEROND,
            _ => throw new ArgumentException($"Invalid status: {status}")
        };
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
