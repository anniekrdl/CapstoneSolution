using System;
using Core.DTOs;
using Data.EF;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class ShoppingCartEF : IShoppingCart
{
    private readonly WebshopContext _webshopContext;
    public ShoppingCartEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;

    }
    public bool AddShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
    {
        ShoppingCartItemEntity shoppingCartItemEntity = shoppingCartItem.ToShoppingCartEntity();

        _webshopContext.ShoppingCartItems.Add(shoppingCartItemEntity);

        int result = _webshopContext.SaveChanges();

        return result > 0;
    }

    public bool EditShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
    {
        _webshopContext.ShoppingCartItems.Update(shoppingCartItem.ToShoppingCartEntity());

        int result = _webshopContext.SaveChanges();
        return result > 0;
    }

    public bool EmptyShoppingCart(List<ShoppingCartItemDTO> items)
    {
        foreach (var item in items)
        {
            var i = item.ToShoppingCartEntity();
            _webshopContext.ShoppingCartItems.Remove(i);
        }
        var result = _webshopContext.SaveChanges();
        return result > 0;
    }

    public List<ShoppingCartItemDTO> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager)
    {
        var items = _webshopContext.ShoppingCartItems
            .Where(item => item.CustomerId == id)
            .ToList();


        var itemDTOs = new List<ShoppingCartItemDTO>();

        foreach (var item in items)
        {
            var product = catalogusManager.GetProductById(item.ProductId);
            var itemDTO = item.ToShoppingCartItemDTO();
            itemDTO.Product = product;
            itemDTOs.Add(itemDTO);
        }


        return itemDTOs;

    }

    public bool RemoveShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
    {
        _webshopContext.ShoppingCartItems.Remove(shoppingCartItem.ToShoppingCartEntity());

        var result = _webshopContext.SaveChanges();
        return result > 0;
    }

    public ShoppingCartItemDTO? SearchById(int Id, ICatalogusManager catalogusManager)
    {
        var item = _webshopContext.ShoppingCartItems.FirstOrDefault(item => item.Id == Id);
        if (item == null)
        {
            return null;
        }

        return item.ToShoppingCartItemDTO();


    }
}
