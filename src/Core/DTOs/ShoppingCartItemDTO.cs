namespace Core.DTOs
{
    public class ShoppingCartItemDTO
    {
        public int? Id { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int NumberOfItems { get; set; }

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