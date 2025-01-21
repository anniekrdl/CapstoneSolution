using Core.DTOs;
namespace Core.Interfaces
{
    public interface IProductItem
    {
        int? Id { get; }
        int ProductId { get; }
        ProductDTO? Product { get; }
        int NumberOfItems { get; }
    }

}