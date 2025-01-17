namespace Core.Models
{
    public class SelectedProductItem
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int NumberOfItems { get; set; }

        public SelectedProductItem(int? id, int productId, Product? product, int numberOfItems)
        {
            Id = id;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
        }

    }

}