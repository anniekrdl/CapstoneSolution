using Core.DTOs;
using Data


.Models;
namespace Logic.Interfaces;

public interface ILoginManager
{
    Task<UserDTO?> UserLogin(string UserName);
}



