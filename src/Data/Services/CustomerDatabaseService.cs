using Core.Interfaces;
using Core.Models;

namespace Data.Services
{

    public class CustomerDatabaseService : ICustomerDatabaseService
    {

        private readonly IDatabaseService _databaseService;

        public CustomerDatabaseService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM klant";

            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                Customer customer = new Customer(
                    id: reader.GetInt32("klant_id"),
                    userName: reader.GetString("gebruikersnaam"),
                    name: reader.GetString("voornaam"),
                    lastname: reader.GetString("achternaam"),
                    email: reader.GetString("email"),
                    street: reader.GetString("straat"),
                    number: reader.GetInt32("huisnummer"),
                    city: reader.GetString("woonplaats")
                );

                customers.Add(customer);

            }

            return customers;

        }

        public async Task AddCustomer(Customer customer)
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

        public async Task<List<Customer>> SearchCustomer(string userName)
        {
            List<Customer> customers = new List<Customer>();
            using var connection = _databaseService.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM klant WHERE gebruikersnaam = @username";
            command.Parameters.AddWithValue("@username", userName);
            var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {

                Customer customer = new Customer(
                    id: reader.GetInt32("klant_id"),
                    userName: reader.GetString("gebruikersnaam"),
                    name: reader.GetString("voornaam"),
                    lastname: reader.GetString("achternaam"),
                    email: reader.GetString("email"),
                    street: reader.GetString("straat"),
                    number: reader.GetInt32("huisnummer"),
                    city: reader.GetString("woonplaats")
                    );

                customers.Add(customer);

            }

            return customers;


        }






    }

}