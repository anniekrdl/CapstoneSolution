using Core.DTOs;
using Data.Models;
namespace Logic.Interfaces;

public interface ICustomerManager
{
    List<CustomerDTO> GetCustomers();
    bool AddCustomer(CustomerDTO customer);
    bool RemoveCustomer(int userId);
    List<CustomerDTO> SearchCustomer(string userName);

    public bool EditCustomer(CustomerDTO customer);

}