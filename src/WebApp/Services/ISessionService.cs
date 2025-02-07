using System;
using Core.DTOs;

namespace WebApp.Services;

public interface ISessionService
{

    UserDTO? GetLoggedInUser(HttpContext context);

}
