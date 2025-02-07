using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface ICustomerManager
{
    List<CustomerDTO> GetCustomers();
    void AddCustomer(CustomerDTO customer);
    void RemoveCustomer(int userId);
    List<CustomerDTO> SearchCustomer(string userName);

}