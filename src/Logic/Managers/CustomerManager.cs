using Core.DTOs;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers
{
    class CustomerManager : ICustomerManager
    {

        private readonly ICustomerDatabaseService _databaseService;

        public CustomerManager(ICustomerDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<List<CustomerDTO>> GetCustomers()
        {
            List<CustomerEntity> customers = await _databaseService.GetCustomers();
            return customers.Select(c => c.ToCustomerDTO()).ToList();

        }

        //add customer to database
        public async Task AddCustomer(CustomerDTO customer)
        {

            await _databaseService.AddCustomer(customer.ToCustomerEntity());
        }

        //remove customer from database
        public async void RemoveCustomer(int userId)
        {
            await _databaseService.RemoveCustomer(userId);
        }


        //search customer in database
        public async Task<List<CustomerDTO>> SearchCustomer(string userName)
        {

            List<CustomerEntity> customers = await _databaseService.SearchCustomer(userName);

            return customers.Select(c => c.ToCustomerDTO()).ToList();

        }

        List<CustomerDTO> ICustomerManager.SearchCustomer(string userName)
        {
            throw new NotImplementedException();
        }
    }

}