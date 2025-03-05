using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface IShoppingCart
{
    bool AddShoppingCartItem(ShoppingCartItemDTO shoppingCartItem);
    bool RemoveShoppingCartItem(ShoppingCartItemDTO shoppingCartItem);
    bool EmptyShoppingCart(List<ShoppingCartItemDTO> items);
    List<ShoppingCartItemDTO> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager);
    ShoppingCartItemDTO? SearchById(int Id, ICatalogusManager catalogusManager);

    bool EditShoppingCartItem(ShoppingCartItemDTO shoppingCartItem);


}