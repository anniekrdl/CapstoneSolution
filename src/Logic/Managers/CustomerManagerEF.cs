using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Core.DTOs;
using Data.EF;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class CustomerManagerEF : ICustomerManager
{
    private readonly WebshopContext _webshopContext;

    public CustomerManagerEF(WebshopContext webshopContext)
    {
        _webshopContext = webshopContext;
    }

    public async Task AddCustomer(CustomerDTO customer)
    {
        await _webshopContext.AddAsync(customer.ToCustomerEntity());
    }

    public Task<List<CustomerDTO>> GetCustomers()
    {
        return _webshopContext.Customers
        .Select(c => c.ToCustomerDTO())
        .ToListAsync();
    }

    public void RemoveCustomer(int userId)
    {
        var user = _webshopContext.Customers.FirstOrDefault(c => c.Id == userId);
        if (user != null)
        {
            _webshopContext.Remove(user.ToCustomerDTO());
        }
    }

    public List<CustomerDTO> SearchCustomer(string userName)
    {

        var users = _webshopContext.Customers.Where(c => c.UserName == userName);


        return users
        .Select(x => x.ToCustomerDTO())
        .ToList();

    }


}
