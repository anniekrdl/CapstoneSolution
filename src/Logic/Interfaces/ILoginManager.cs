using Core.Models;
namespace Logic.Interfaces;

public interface ILoginManager
{
    Task<User?> UserLogin(string UserName);
}



