using Core.Interfaces;
using Core.Models;
namespace Data.Services
{

    public class OrderItemDatabaseService : IOrderItemDatabaseService
    {
        private readonly IDatabaseService _databaseService;

        public OrderItemDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

        }

        public async Task<List<OrderItem>> GetOrderItems()
        {

            List<OrderItem> orderItems = new List<OrderItem>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM bestelling_detail";

            var reader = command.ExecuteReader();
            while (reader.Read())
            {


                OrderItem orderItem = new OrderItem(
                    reader.GetInt32("detail_id"),
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("product_id"),
                    reader.GetInt32("aantal"),
                    null

                );

                orderItems.Add(orderItem);

            }
            return orderItems;

        }

        public async Task<List<OrderItem>> GetOrderItemByOrderId(int Id)
        {
            //order_id

            List<OrderItem> orderList = new List<OrderItem>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM bestelling_detail WHERE bestelling_id = @id";

            command.Parameters.AddWithValue("@id", Id);


            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                orderList.Add(new OrderItem(
                    reader.GetInt32("detail_id"),
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("product_id"),
                    reader.GetInt32("aantal"),
                    null

                ));
            }
            return orderList;
        }

        public async Task<OrderItem> GetOrderItemById(int Id)
        {
            //detail_id

            List<OrderItem> orderList = new List<OrderItem>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM bestelling_detail WHERE detail_id = @id LIMIT 1";

            command.Parameters.AddWithValue("@id", Id);


            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                orderList.Add(new OrderItem(
                    reader.GetInt32("detail_id"),
                    reader.GetInt32("bestelling_id"),
                    reader.GetInt32("product_id"),
                    reader.GetInt32("aantal"),
                    null

                ));
            }
            return orderList[0];
        }



        public async Task<bool> AddOrderItem(OrderItem orderItem)
        {


            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO bestelling_detail (bestelling_id, product_id, aantal) VALUES (@orderId, @productId, @numberOfItems)";

                DateOnly date = DateOnly.FromDateTime(DateTime.Now);



                command.Parameters.AddWithValue("@orderId", orderItem.OrderId);
                command.Parameters.AddWithValue("@productId", orderItem.ProductId);
                command.Parameters.AddWithValue("@numberOfItems", orderItem.NumberOfItems);


                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }


        }


        public async Task<bool> UpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();



                var command = connection.CreateCommand();
                command.CommandText = "UPDATE bestelling_detail SET bestelling_id = @orderId, product_id = @productId, aantal = @numberOfItems WHERE detail_id = @id";

                command.Parameters.AddWithValue("@id", orderItem.Id);
                command.Parameters.AddWithValue("@orderId", orderItem.OrderId);
                command.Parameters.AddWithValue("@productId", orderItem.ProductId);
                command.Parameters.AddWithValue("@numberOfItems", orderItem.NumberOfItems);



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