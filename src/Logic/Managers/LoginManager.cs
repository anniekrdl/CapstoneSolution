using Core.DTOs;
using Data.Interfaces;
using Data.Models;
using Logic.Interfaces;
using Logic.Mappers;

namespace Logic.Managers;

public class LoginManager : ILoginManager
{

    private readonly ILoginService _loginService;

    public LoginManager(ILoginService loginService)
    {
        _loginService = loginService;
    }


    public async Task<UserDTO?> UserLogin(string username)
    {
        try
        {
            if (username == "admin")
            {
                return new AdministratorDTO();
            }
            else
            {
                // Probeer in te loggen
                CustomerEntity? loggedInCustomer = await _loginService.Login(username);


                //entity to DTO

                return loggedInCustomer?.ToCustomerDTO();

            }

        }
        catch (Exception ex)
        {

            Console.WriteLine($"Er is een fout opgetreden tijdens het inloggen: {ex.Message}");
            return null;


        }


    }

    UserDTO? ILoginManager.UserLogin(string UserName)
    {
        throw new NotImplementedException();
    }
}