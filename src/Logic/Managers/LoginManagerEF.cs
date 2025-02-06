using System;
using Core.DTOs;
using Data.EF;
using Logic.Interfaces;

namespace Logic.Managers;

public class LoginManagerEF : ILoginManager
{
    private readonly WebshopContext _webshopContext;
    private readonly ICustomerManager _customerManager;

    public LoginManagerEF(WebshopContext webshopContext, ICustomerManager customerManager)
    {
        _webshopContext = webshopContext;
        _customerManager = customerManager;
    }
    public UserDTO? UserLogin(string UserName)
    {
        if (UserName == "admin")
        {
            //go to admin
            return new AdministratorDTO();
        }
        else
        {

            var customers = _customerManager.SearchCustomer(UserName);


            if (customers.Count() > 0)
            {
                return customers.FirstOrDefault();
            }



        }

        return null;
    }
}
