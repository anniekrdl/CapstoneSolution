using Data.Interfaces;
using Data.Models;

namespace Data.Services
{

    public class CategoryDatabaseService : ICategoryDatabaseService
    {

        private readonly IDatabaseService _databaseService;

        public CategoryDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

        }



        public async Task<List<CategoryEntity>> GetAllCategories()
        {

            List<CategoryEntity> categories = new List<CategoryEntity>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM categorie";

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {

                CategoryEntity category = new CategoryEntity(
                    reader.GetInt32("categorie_id"),
                    reader.GetString("naam"),
                    reader.GetString("beschrijving")

                );

                categories.Add(category);



            }

            return categories;

        }

        public async Task AddCategory(CategoryEntity category)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO categorie (naam, beschrijving) VALUES (@name, @description);";

            command.Parameters.AddWithValue("@name", category.Name);
            command.Parameters.AddWithValue("@description", category.Description);

            await command.ExecuteNonQueryAsync();
        }

        public async Task RemoveCategory(CategoryEntity category)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM categorie WHERE categorie_id = @id";

            Console.WriteLine($"de id is {category.Id}");


            command.Parameters.AddWithValue("@id", category.Id);

            await command.ExecuteNonQueryAsync();

        }

        public async Task<List<CategoryEntity>> SearchCategory(string searchTerm)
        {

            List<CategoryEntity> categories = new List<CategoryEntity>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM categorie WHERE naam LIKE CONCAT('%', @searchTerm, '%')";

            command.Parameters.AddWithValue("@searchTerm", searchTerm);

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var category = new CategoryEntity(
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