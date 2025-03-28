using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.DTOs;

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
        public string OrderStatus { get; private set; }

        [NotMapped]
        public OrderStatusDTO OrderStatusEnum
        {
            get => Enum.Parse<OrderStatusDTO>(OrderStatus);
            private set => OrderStatus = value.ToString();
        }

        public OrderEntity() { }
        public OrderEntity(int? id, int customerId, DateOnly? date, OrderStatusDTO orderStatus)
        {
            Id = id;
            CustomerId = customerId;
            Date = date;
            OrderStatusEnum = orderStatus;

        }

        public void UpdateOrderStatus(OrderStatusDTO orderStatus)
        {
            OrderStatusEnum = orderStatus;
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