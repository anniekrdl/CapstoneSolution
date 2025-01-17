using Core.Models;
namespace Core.Interfaces;

public interface IShoppingCart
{
    Task<bool> AddShoppingCartItem(ShoppingCartItem shoppingCartItem);
    Task<bool> RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem);
    Task<bool> EmptyShoppingCart(List<ShoppingCartItem> items);
    Task<List<ShoppingCartItem>> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager);
    Task<ShoppingCartItem?> SearchById(int Id, ICatalogusManager catalogusManager);


}