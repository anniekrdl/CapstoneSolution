using Core.Models;
namespace Core.Interfaces
{
    public interface IProductItem
    {
        int? Id { get; }
        int ProductId { get; }
        Product? Product { get; }
        int NumberOfItems { get; }
    }

}