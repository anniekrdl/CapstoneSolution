using Core.DTOs;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;
namespace Logic.Managers
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ICartDatabaseService _cartDatabaseService;

        public ShoppingCart(ICartDatabaseService cartDatabaseService)
        {

            _cartDatabaseService = cartDatabaseService;
        }
        public async Task<bool> AddShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
        {
            try
            {
                // DTO to entity
                await _cartDatabaseService.AddShoppingCartItem(shoppingCartItem.ToShoppingCartEntity());
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
        {
            try
            {
                await _cartDatabaseService.RemoveShoppingCartItem(shoppingCartItem.ToShoppingCartEntity());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ShoppingCartItemDTO?> SearchById(int Id, ICatalogusManager catalogusManager)
        {
            ShoppingCartItemEntity? shoppingCartItem = await _cartDatabaseService.SearchById(Id);

            if (shoppingCartItem != null)
            {
                // add Product
                ProductDTO? product = catalogusManager.GetProductById(shoppingCartItem.ProductId);

                if (product != null)
                {
                    shoppingCartItem.setProduct(product.ToProductEntity());
                }
                else
                {
                    Console.WriteLine($"Product met ID {shoppingCartItem.ProductId} niet gevonden.");
                }

            }

            return shoppingCartItem?.ToShoppingCartItemDTO();

        }

        public async Task<bool> EmptyShoppingCart(List<ShoppingCartItemDTO> items)
        {

            try
            {
                foreach (ShoppingCartItemDTO item in items)
                {

                    await _cartDatabaseService.RemoveShoppingCartItem(item.ToShoppingCartEntity());

                }

                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<List<ShoppingCartItemDTO>> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager)
        {

            List<ShoppingCartItemEntity> shoppingCartItems = await _cartDatabaseService.GetAllShoppingCartItemsByCustomerId(id);

            foreach (ShoppingCartItemEntity item in shoppingCartItems)
            {

                ProductDTO? p = catalogusManager.GetProductById(item.ProductId);

                if (p != null)
                {

                    item.setProduct(p.ToProductEntity());

                }

            }
            return shoppingCartItems.Select(item => item.ToShoppingCartItemDTO()).ToList();

        }

        bool IShoppingCart.AddShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        bool IShoppingCart.RemoveShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        bool IShoppingCart.EmptyShoppingCart(List<ShoppingCartItemDTO> items)
        {
            throw new NotImplementedException();
        }

        List<ShoppingCartItemDTO> IShoppingCart.GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager)
        {
            throw new NotImplementedException();
        }

        ShoppingCartItemDTO? IShoppingCart.SearchById(int Id, ICatalogusManager catalogusManager)
        {
            throw new NotImplementedException();
        }

        public bool EditShoppingCartItem(ShoppingCartItemDTO shoppingCartItem)
        {
            throw new NotImplementedException();
        }

        public bool RemoveShoppingCartItemById(int shoppingCartItemId, ICatalogusManager catalogusManager)
        {
            throw new NotImplementedException();
        }
    }

}