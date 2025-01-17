using Core.Interfaces;
using Core.Models;
using MySqlConnector;

namespace Data.Services
{

    public class ProductDatabaseService : IProductDatabaseService
    {

        private readonly IDatabaseService _databaseService;
        public ProductDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

        }


        public async Task<List<Product>> GetAllProducts()
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();
            var query = "SELECT * FROM product";
            var command = new MySqlCommand(query, connection);
            var reader = await command.ExecuteReaderAsync();
            var products = new List<Product>();
            while (await reader.ReadAsync())
            {
                products.Add(new Product(
                 reader.GetInt32("product_id"),
                 reader.GetString("naam"),
                 reader.GetString("beschrijving"),
                 reader.GetInt32("prijs"),
                 reader.GetInt32("voorraad"),
                 reader.GetInt32("categorie_id"),
                 reader.GetString("afbeelding_url")
                 ));


            }

            return products;

        }

        public async Task<bool> AddProduct(Product product)
        {

            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();

                command.CommandText = "INSERT INTO product (naam, beschrijving,categorie_id, prijs, voorraad, afbeelding_url) VALUES (@name, @description,@category_id, @price, @stock, @image_url)";

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@image_url", product.ImageUrl);
                command.Parameters.AddWithValue("@category_id", product.CategoryId);

                await command.ExecuteNonQueryAsync();
                return true;

            }
            catch
            {
                Console.WriteLine("product kon niet worden toegevoegd");
                return false;

            }


        }

        public async Task<bool> EditProduct(Product product)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();

                command.CommandText = "UPDATE product SET naam = @name, beschrijving = @description, prijs = @price, voorraad = @stock, afbeelding_url = @image_url WHERE product_id = @id";

                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@image_url", product.ImageUrl);
                command.Parameters.AddWithValue("@id", product.Id);

                await command.ExecuteNonQueryAsync();
                return true;


            }
            catch (Exception ex)
            {

                Console.WriteLine("Error: " + ex.Message);
                return false;


            }

        }

        public async Task<bool> DeleteProduct(int product_id)
        {
            try
            {
                using var connection = _databaseService.GetConnection();
                await connection.OpenAsync();

                using var command = connection.CreateCommand();

                command.CommandText = "DELETE FROM product WHERE product_id = @id";

                command.Parameters.AddWithValue("@id", product_id);

                await command.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"error: {ex.Message}");
                return false;
            }


        }

        public async Task<List<Product>> SearchProductById(int Id)
        {
            List<Product> products = new List<Product>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM product WHERE product_id = @id";

            command.Parameters.AddWithValue("@id", Id);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product(
                    reader.GetInt32("product_id"),
                    reader.GetString("naam"),
                    reader.GetString("beschrijving"),
                    reader.GetInt32("prijs"),
                    reader.GetInt32("voorraad"),
                    reader.GetInt32("categorie_id"),
                    reader.GetString("afbeelding_url")
                );

                products.Add(product);
            }

            return products;


        }

        public async Task<List<Product>> SearchProductBySearchTerm(string searchTerm)
        {
            List<Product> products = new List<Product>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM product WHERE naam LIKE CONCAT('%', @searchTerm, '%')";

            command.Parameters.AddWithValue("@searchTerm", searchTerm);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var product = new Product(
                    reader.GetInt32("product_id"),
                    reader.GetString("naam"),
                    reader.GetString("beschrijving"),
                    reader.GetInt32("prijs"),
                    reader.GetInt32("voorraad"),
                    reader.GetInt32("categorie_id"),
                    reader.GetString("afbeelding_url")
                );

                products.Add(product);
            }

            return products;


        }

    }
}