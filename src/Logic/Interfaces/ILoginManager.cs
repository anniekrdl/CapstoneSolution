using Core.DTOs;
using Data


.Models;
namespace Logic.Interfaces;

public interface ILoginManager
{
    UserDTO? UserLogin(string UserName);
}



