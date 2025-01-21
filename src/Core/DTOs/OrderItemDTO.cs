using Core.Interfaces;
namespace Core.DTOs
{

    public class OrderItemDTO : IProductItem
    {
        public int? Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public ProductDTO? Product { get; private set; }
        public int NumberOfItems { get; private set; }



        public OrderItemDTO(int? id, int orderId, int productId, int numberOfItems, ProductDTO? product)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Product = product;
            NumberOfItems = numberOfItems;
        }
    }

}