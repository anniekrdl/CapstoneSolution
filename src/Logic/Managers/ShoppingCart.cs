using Core.Models;
using Core.Interfaces;
using Data.Services;
namespace Logic.Managers
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ICartDatabaseService _cartDatabaseService;

        public ShoppingCart(ICartDatabaseService cartDatabaseService)
        {

            _cartDatabaseService = cartDatabaseService;
        }
        public async Task<bool> AddShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                await _cartDatabaseService.AddShoppingCartItem(shoppingCartItem);
                return true;

            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveShoppingCartItem(ShoppingCartItem shoppingCartItem)
        {
            try
            {
                await _cartDatabaseService.RemoveShoppingCartItem(shoppingCartItem);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<ShoppingCartItem?> SearchById(int Id, ICatalogusManager catalogusManager)
        {
            ShoppingCartItem? shoppingCartItem = await _cartDatabaseService.SearchById(Id);

            if (shoppingCartItem != null)
            {
                // add Product
                Product? product = await catalogusManager.GetProductById(shoppingCartItem.ProductId);

                if (product != null)
                {
                    shoppingCartItem.setProduct(product);
                }
                else
                {
                    Console.WriteLine($"Product met ID {shoppingCartItem.ProductId} niet gevonden.");
                }

            }

            return shoppingCartItem;

        }

        public async Task<bool> EmptyShoppingCart(List<ShoppingCartItem> items)
        {

            try
            {
                foreach (ShoppingCartItem item in items)
                {
                    await _cartDatabaseService.RemoveShoppingCartItem(item);

                }

                return true;
            }
            catch
            {
                return false;
            }


        }

        public async Task<List<ShoppingCartItem>> GetAllItemsByCustomerId(int id, ICatalogusManager catalogusManager)
        {


            List<ShoppingCartItem> shoppingCartItems = await _cartDatabaseService.GetAllShoppingCartItemsByCustomerId(id);


            foreach (ShoppingCartItem item in shoppingCartItems)
            {

                Product? p = await catalogusManager.GetProductById(item.ProductId);

                if (p != null)
                {

                    item.setProduct(p);

                }



            }
            return shoppingCartItems;

        }

    }

}