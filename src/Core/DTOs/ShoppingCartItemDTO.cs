using Core.Interfaces;
namespace Core.DTOs
{
    public class ShoppingCartItemDTO : IProductItem
    {
        public int? Id { get; set; }
        public int CustomerId { get; private set; }
        public int ProductId { get; private set; }
        public ProductDTO? Product { get; private set; }
        public int NumberOfItems { get; private set; }

        public ShoppingCartItemDTO(int? id, int customerId, int productId, ProductDTO? product, int numberOfItems)
        {
            CustomerId = customerId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
            Id = id;
        }

        public void setProduct(ProductDTO product)
        {
            Product = product;

        }



    }
}