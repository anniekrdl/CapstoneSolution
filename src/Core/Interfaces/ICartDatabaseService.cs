using System;
using Core.Models;

namespace Core.Interfaces;

public interface ICartDatabaseService
{
    // Get all shoppingcart item by Customer Id
    Task<List<ShoppingCartItem>> GetAllShoppingCartItemsByCustomerId(int Id);

    // Add shoppingcart item
    Task<bool> AddShoppingCartItem(ShoppingCartItem shoppingCartItem);

    // Remove shoppingcart item
    Task<bool> RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem);

    // Search shopping Cart Item by Id
    Task<ShoppingCartItem?> SearchById(int Id);



}
