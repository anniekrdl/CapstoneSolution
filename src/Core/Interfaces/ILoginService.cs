using System;
using Core.Models;

namespace Core.Interfaces;

public interface ILoginService
{

    Task<User?> Login(string UserName);

}
