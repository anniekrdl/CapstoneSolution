using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    [Table("bestelling")]
    public class OrderEntity
    {

        [Column("bestelling_id")]
        [Key, Required]
        public int? Id { get; private set; }
        [Column("klant_id")]
        [ForeignKey("klant")]
        [Required]
        public int CustomerId { get; private set; }
        [Column("datum")]
        [Required]
        public DateOnly? Date { get; private set; }
        [Column("status")]
        [Required]
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