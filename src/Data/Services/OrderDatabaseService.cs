using Core.Models;
using Core.Interfaces;
namespace Data.Services
{

    public class OrderDatabaseService : IOrderDatabaseService
    {

        private readonly IDatabaseService _databaseService;

        public OrderDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Order>> GetOrders()
        {

            List<Order> orders = new List<Order>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM bestelling";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {

                Order order = new Order(
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("klant_id"),
                    reader.GetDateOnly("datum"),
                    Enum.Parse<OrderStatus>(reader.GetString("status"))
                );

                orders.Add(order);

            }
            return orders;

        }

        public async Task<List<int>> GetOrderIdByCustomerId(int customerId)
        {
            List<int> customersFound = new List<int>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM bestelling WHERE klant_id = @customerId";

            command.Parameters.AddWithValue("@customerId", customerId);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customersFound.Add(reader.GetInt32("bestelling_id"));
            }

            return customersFound;
        }

        public async Task<List<Order>> GetOrdersByCustomerId(int customerId)
        {
            List<Order> orders = new List<Order>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM bestelling WHERE klant_id = @customerId";

            command.Parameters.AddWithValue("@customerId", customerId);

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Order order = new Order(
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("klant_id"),
                    reader.GetDateOnly("datum"),
                    Enum.Parse<OrderStatus>(reader.GetString("status"))
                );

                orders.Add(order);

            }

            return orders;
        }

        public async Task<Order?> GetOrdersByOrderId(int Id)
        {

            List<Order> orderList = new List<Order>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM bestelling WHERE bestelling_id = @id LIMIT 1";

            command.Parameters.AddWithValue("@id", Id);


            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                orderList.Add(new Order(
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("klant_id"),
                    DateOnly.FromDateTime(reader.GetDateTime("datum")),
                    Enum.Parse<OrderStatus>(reader.GetString("status"))
                ));
            }

            if (orderList.Count > 0)
            {
                return orderList[0];
            }
            else
            {
                //Lege lijst terug. Null voor error 
                return null;
            }
        }

        public async Task<bool> AddOrder(Order order)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO bestelling (bestelling_id, klant_id, datum, status) VALUES(@id, @customer_id, @date, @status)";

                DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                command.Parameters.AddWithValue("@id", order.Id);
                command.Parameters.AddWithValue("@customer_id", order.CustomerId);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@status", order.OrderStatus.ToString());


                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }


        }

        public async Task<bool> UpdateOrder(Order order)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "UPDATE bestelling SET klant_id = @customer_id, datum = @date, status = @status WHERE bestelling_id = @id";

                command.Parameters.AddWithValue("@id", order.Id);
                command.Parameters.AddWithValue("@customer_id", order.CustomerId);
                command.Parameters.AddWithValue("@date", order.Date);
                command.Parameters.AddWithValue("@status", order.OrderStatus.ToString());

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (System.Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }






    }

}