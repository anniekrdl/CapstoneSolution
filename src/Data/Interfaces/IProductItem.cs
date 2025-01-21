using Data.Models;
namespace Data.Interfaces
{
    public interface IProductItem
    {
        int? Id { get; }
        int ProductId { get; }
        ProductEntity? Product { get; }
        int NumberOfItems { get; }
    }

}