
using Core.Models;
using Logic.Interfaces;
using Core.Interfaces;

namespace Logic.Managers;

public class LoginManager : ILoginManager
{

    private readonly ILoginService _loginService;

    public LoginManager(ILoginService loginService)
    {
        _loginService = loginService;
    }


    public async Task<User?> UserLogin(string username)
    {
        try
        {

            User? loggedInCustomer = null;
            // Probeer in te loggen
            loggedInCustomer = await _loginService.Login(username);

            return loggedInCustomer;


        }
        catch (Exception ex)
        {

            Console.WriteLine($"Er is een fout opgetreden tijdens het inloggen: {ex.Message}");
            return null;


        }


    }
}