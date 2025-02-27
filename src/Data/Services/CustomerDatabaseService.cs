using Data.Interfaces;
using Data.Models;

namespace Data.Services
{

    public class CustomerDatabaseService : ICustomerDatabaseService
    {

        private readonly IDatabaseService _databaseService;

        public CustomerDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<CustomerEntity>> GetCustomers()
        {
            List<CustomerEntity> customers = new List<CustomerEntity>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM klant";

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                CustomerEntity customer = new CustomerEntity
                {
                    Id = reader.GetInt32("klant_id"),
                    UserName = reader.GetString("gebruikersnaam"),
                    Name = reader.GetString("voornaam"),
                    LastName = reader.GetString("achternaam"),
                    Email = reader.GetString("email"),
                    Street = reader.GetString("straat"),
                    Number = reader.GetInt32("huisnummer"),
                    City = reader.GetString("woonplaats")
                };

                customers.Add(customer);

            }

            return customers;

        }

        public async Task AddCustomer(CustomerEntity customer)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO klant (gebruikersnaam, voornaam, achternaam, email, straat, huisnummer, woonplaats, toevoeging) VALUES (@username, @firstname, @lastname, @email, @street, @number, @city, @addition);";

            command.Parameters.AddWithValue("@username", customer.UserName);
            command.Parameters.AddWithValue("@firstname", customer.Name);
            command.Parameters.AddWithValue("@lastname", customer.LastName);
            command.Parameters.AddWithValue("@email", customer.Email);
            command.Parameters.AddWithValue("@street", customer.Street);
            command.Parameters.AddWithValue("@number", customer.Number);
            command.Parameters.AddWithValue("@city", customer.City);
            command.Parameters.AddWithValue("@addition", customer.Addition);


            await command.ExecuteNonQueryAsync();

        }

        public async Task RemoveCustomer(int userId)
        {
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM klant WHERE klant_id = @klantId";

            command.Parameters.AddWithValue("@klantId", userId);

            await command.ExecuteNonQueryAsync();
        }

        public async Task<List<CustomerEntity>> SearchCustomer(string userName)
        {
            List<CustomerEntity> customers = new List<CustomerEntity>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM klant WHERE gebruikersnaam = @username";
            command.Parameters.AddWithValue("@username", userName);
            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {

                CustomerEntity customer = new CustomerEntity
                {
                    Id = reader.GetInt32("klant_id"),
                    UserName = reader.GetString("gebruikersnaam"),
                    Name = reader.GetString("voornaam"),
                    LastName = reader.GetString("achternaam"),
                    Email = reader.GetString("email"),
                    Street = reader.GetString("straat"),
                    Number = reader.GetInt32("huisnummer"),
                    City = reader.GetString("woonplaats")
                };


                customers.Add(customer);

            }

            return customers;


        }






    }

}