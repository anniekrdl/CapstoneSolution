using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;
namespace Data.Models
{

    [Table("bestelling_detail")]
    public class OrderItemEntity : IProductItem
    {
        [Column("detail_id")]
        [Key, Required]
        public int? Id { get; private set; }
        [Column("bestelling_id")]
        [ForeignKey("bestelling")]
        [Required]
        public int OrderId { get; private set; }
        [Column("product_id")]
        [ForeignKey("product")]
        [Required]
        public int ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }
        [Column("aantal")]
        [Required]
        public int NumberOfItems { get; private set; }
        public OrderItemEntity() { }

        public OrderItemEntity(int? id, int orderId, int productId, int numberOfItems, ProductEntity? product)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
        }
    }

}