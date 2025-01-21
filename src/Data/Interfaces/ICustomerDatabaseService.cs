using System;
using Data.Models;

namespace Data.Interfaces;

public interface ICustomerDatabaseService
{
    // Get all customers
    Task<List<CustomerEntity>> GetCustomers();

    // Add customer
    Task AddCustomer(CustomerEntity customer);

    // Remove customer
    Task RemoveCustomer(int userId);

    // Search customer by name
    Task<List<CustomerEntity>> SearchCustomer(string userName);



}
