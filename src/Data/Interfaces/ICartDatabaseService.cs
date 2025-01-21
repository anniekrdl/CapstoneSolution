using System;
using Data.Models;

namespace Data.Interfaces;

public interface ICartDatabaseService
{
    // Get all shoppingcart item by Customer Id
    Task<List<ShoppingCartItemEntity>> GetAllShoppingCartItemsByCustomerId(int Id);

    // Add shoppingcart item
    Task<bool> AddShoppingCartItem(ShoppingCartItemEntity shoppingCartItem);

    // Remove shoppingcart item
    Task<bool> RemoveShoppingCartItem(ShoppingCartItemEntity shoppingCartItem);

    // Search shopping Cart Item by Id
    Task<ShoppingCartItemEntity?> SearchById(int Id);



}
