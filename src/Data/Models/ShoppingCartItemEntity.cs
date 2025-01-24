using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Interfaces;
namespace Data.Models
{
    [Table("huidige_bestelling_item")]
    public class ShoppingCartItemEntity : IProductItem
    {
        [Column("huidige_bestelling_id")]
        [Key, Required]
        public int? Id { get; set; }
        [Column("klant_id")]
        [ForeignKey("klant")]
        [Required]
        public int CustomerId { get; private set; }
        [Column("product_id")]
        [ForeignKey("product")]
        [Required]
        public int ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }
        [Column("aantal")]
        [Required, MinLength(1)]
        public int NumberOfItems { get; private set; }

        public ShoppingCartItemEntity(int? id, int customerId, int productId, ProductEntity? product, int numberOfItems)
        {
            CustomerId = customerId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
            Id = id;
        }

        public void setProduct(ProductEntity product)
        {
            Product = product;

        }



    }
}