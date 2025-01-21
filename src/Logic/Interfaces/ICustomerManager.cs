using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface ICustomerManager
{
    Task<List<CustomerDTO>> GetCustomers();
    Task AddCustomer(CustomerDTO customer);
    void RemoveCustomer(int userId);
    Task<List<CustomerDTO>> SearchCustomer(string userName);

}