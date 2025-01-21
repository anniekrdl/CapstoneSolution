namespace Data.Models
{
    public class OrderEntity
    {

        public int? Id { get; private set; }
        public int CustomerId { get; private set; }
        public DateOnly? Date { get; private set; }
        public OrderStatus OrderStatus { get; private set; }


        public OrderEntity(int? id, int customerId, DateOnly? date, OrderStatus orderStatus)
        {
            Id = id;
            CustomerId = customerId;
            Date = date;
            OrderStatus = orderStatus;

        }

        public void UpdateOrderStatus(OrderStatus orderStatus)
        {
            OrderStatus = orderStatus;
        }



    }



    public enum OrderStatus
    {
        AANGEMAAKT,
        GEPLAATST,
        GEACCEPTEERD,
        GEWEIGERD,
        AFGEROND

    }

}