using Data.Interfaces;
namespace Data.Models
{

    public class OrderItemEntity : IProductItem
    {
        public int? Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public ProductEntity? Product { get; private set; }
        public int NumberOfItems { get; private set; }



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