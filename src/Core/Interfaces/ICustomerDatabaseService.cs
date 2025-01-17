using System;
using Core.Models;

namespace Core.Interfaces;

public interface ICustomerDatabaseService
{
    // Get all customers
    Task<List<Customer>> GetCustomers();

    // Add customer
    Task AddCustomer(Customer customer);

    // Remove customer
    Task RemoveCustomer(int userId);

    // Search customer by name
    Task<List<Customer>> SearchCustomer(string userName);



}
