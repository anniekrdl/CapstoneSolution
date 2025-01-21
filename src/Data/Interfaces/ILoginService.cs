using System;
using Data.Models;

namespace Data.Interfaces;

public interface ILoginService
{

    Task<CustomerEntity?> Login(string UserName);

}
