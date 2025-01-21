using Data.Interfaces;
namespace Data.Models
{
    public class ShoppingCartItemEntity : IProductItem
    {
        public int? Id { get; set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }
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