using System;
using Core.DTOs;

namespace WebApp.Managers;

public interface ISessionManager
{
    UserDTO? LoggedInUser { get; set; }
    bool IsLoggedIn { get; }

}
