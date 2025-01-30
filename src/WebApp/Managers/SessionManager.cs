using System;
using Core.DTOs;

namespace WebApp.Managers;

public class SessionManager : ISessionManager
{
    public UserDTO? LoggedInUser { get; set; }

    public bool IsLoggedIn => LoggedInUser != null;
}
