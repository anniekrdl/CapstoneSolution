using Core.Interfaces;
namespace Core.Models
{
    public class ShoppingCartItem : IProductItem
    {
        public int? Id { get; set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int NumberOfItems { get; private set; }

        public ShoppingCartItem(int? id, int customerId, int productId, Product? product, int numberOfItems)
        {
            CustomerId = customerId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
            Id = id;
        }

        public void setProduct(Product product)
        {
            Product = product;

        }



    }
}