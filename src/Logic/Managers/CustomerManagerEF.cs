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

    public bool AddCustomer(CustomerDTO customer)
    {
        try
        {
            _webshopContext.Add(customer.ToCustomerEntity());
            _webshopContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }

    }

    public List<CustomerDTO> GetCustomers()
    {
        return _webshopContext.Customers
        .Select(c => c.ToCustomerDTO())
        .ToList();
    }

    public bool RemoveCustomer(int userId)
    {
        try
        {

            var user = _webshopContext.Customers.FirstOrDefault(c => c.Id == userId);


            if (user != null)
            {
                _webshopContext.Remove(user);
                _webshopContext.SaveChanges();
                return true;
            }
            return false;

        }
        catch
        {
            return false;
        }


    }

    public List<CustomerDTO> SearchCustomer(string userName)
    {

        var users = _webshopContext.Customers.Where(c => c.UserName == userName);


        return users
        .Select(x => x.ToCustomerDTO())
        .ToList();

    }

    public bool EditCustomer(CustomerDTO customer)
    {
        var existingCustomer = _webshopContext.Customers.FirstOrDefault(c => c.Id == customer.Id);
        if (existingCustomer != null)
        {
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Email = customer.Email;
            existingCustomer.Name = customer.Name;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Street = customer.Street;
            existingCustomer.Number = customer.Number;
            existingCustomer.Addition = customer.Addition;
            existingCustomer.City = customer.City;
            // Update other properties as needed

            _webshopContext.Update(existingCustomer);
            _webshopContext.SaveChanges();
            return true;
        }
        return false;
    }


}
