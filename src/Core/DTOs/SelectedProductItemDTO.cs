namespace Core.DTOs
{
    public class SelectedProductItemDTO
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int NumberOfItems { get; set; }

        public SelectedProductItemDTO(int? id, int productId, ProductDTO? product, int numberOfItems)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
        }

    }

}