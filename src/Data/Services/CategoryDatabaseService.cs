using Core.Interfaces;
using Core.Models;
namespace Data.Services
{

    public class CategoryDatabaseService : ICategoryDatabaseService
    {

        private readonly IDatabaseService _databaseService;

        public CategoryDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

        }



        public async Task<List<Category>> GetAllCategories()
        {

            List<Category> categories = new List<Category>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM categorie";

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {

                Category category = new Category(
                    reader.GetInt32("categorie_id"),
                    reader.GetString("naam"),
                    reader.GetString("beschrijving")

                );

                categories.Add(category);



            }

            return categories;

        }

        public async Task AddCategory(Category category)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO categorie (naam, beschrijving) VALUES (@name, @description);";

            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@description", category.Description);

            await command.ExecuteNonQueryAsync();
        }

        public async Task RemoveCategory(Category category)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM categorie WHERE categorie_id = @id";

            Console.WriteLine($"de id is {category.Id}");


            command.Parameters.AddWithValue("@id", category.Id);

            await command.ExecuteNonQueryAsync();

        }

        public async Task<List<Category>> SearchCategory(string searchTerm)
        {

            List<Category> categories = new List<Category>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM categorie WHERE naam LIKE CONCAT('%', @searchTerm, '%')";

            command.Parameters.AddWithValue("@searchTerm", searchTerm);

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var category = new Category(
                    reader.GetInt32("categorie_id"),
                    reader.GetString("naam"),
                    reader.GetString("beschrijving")
                );

                categories.Add(category);

            }

            return categories;

        }



    }
}