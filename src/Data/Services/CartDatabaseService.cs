using Data.Interfaces;
using Data.Models;
namespace Data.Services
{
    public class CartDatabaseService : ICartDatabaseService
    {
        private readonly IDatabaseService _databaseService;
        public CartDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

        }
        public async Task<List<ShoppingCartItemEntity>> GetAllShoppingCartItemsByCustomerId(int Id)
        {
            var items = new List<ShoppingCartItemEntity>();
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM huidige_bestelling_item WHERE klant_id = @customerId";
                command.Parameters.AddWithValue("@customerId", Id);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {

                    var item = new ShoppingCartItemEntity(
                        reader.GetInt32("huidige_bestelling_id"),
                        reader.GetInt32("klant_id"),
                        reader.GetInt32("product_id"),
                        null,
                        reader.GetInt32("aantal")
                    );



                    items.Add(item);

                }


            }
            catch (Exception ex)
            {

                Console.WriteLine($"Fout bij ophalen van winkelwagenitems: {ex.Message}");

            }

            return items;



        }

        public async Task<bool> AddShoppingCartItem(ShoppingCartItemEntity shoppingCartItem)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO huidige_bestelling_item (klant_id, product_id, aantal) VALUES (@customerId, @productId, @Number)";

                command.Parameters.AddWithValue("@customerId", shoppingCartItem.CustomerId);
                command.Parameters.AddWithValue("@productId", shoppingCartItem.ProductId);
                command.Parameters.AddWithValue("@Number", shoppingCartItem.NumberOfItems);

                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij toevoegen van winkelwagenitem: {ex.Message}");
                return false;



            }

        }

        public async Task<bool> RemoveShoppingCartItem(ShoppingCartItemEntity shoppingCartItem)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM huidige_bestelling_item WHERE huidige_bestelling_id = @Id";

                command.Parameters.AddWithValue("@Id", shoppingCartItem.Id);

                await command.ExecuteNonQueryAsync();
                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij verwijderen van winkelwagenitem: {ex.Message}");
                return false;
            }
        }

        public async Task<ShoppingCartItemEntity?> SearchById(int Id)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM huidige_bestelling_item WHERE huidige_bestelling_id = @Id";

                command.Parameters.AddWithValue("@Id", Id);

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new ShoppingCartItemEntity(
                        reader.GetInt32("huidige_bestelling_id"),
                        reader.GetInt32("klant_id"),
                        reader.GetInt32("product_id"),
                        null,
                        reader.GetInt32("aantal")
                    );

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fout bij zoeken van winkelwagenitem: {ex.Message}");
            }

            return null;









        }

    }


}