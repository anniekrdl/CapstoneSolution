using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface IShoppingCart
{
    Task<bool> AddShoppingCartItem(ShoppingCartItemDTO shoppingCartItem);
    Task<bool> RemoveShoppingCartItem(ShoppingCartItemDTO shoppingCartItem);
    Task<bool> EmptyShoppingCart(List<ShoppingCartItemDTO> items);
    Task<List<ShoppingCartItemDTO>> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager);
    Task<ShoppingCartItemDTO?> SearchById(int Id, ICatalogusManager catalogusManager);


}