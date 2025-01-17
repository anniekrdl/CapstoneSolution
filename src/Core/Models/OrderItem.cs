using Core.Interfaces;
namespace Core.Models
{

    public class OrderItem : IProductItem
    {
        public int? Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public Product? Product { get; private set; }
        public int NumberOfItems { get; private set; }



        public OrderItem(int? id, int orderId, int productId, int numberOfItems, Product? product)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
        }
    }

}