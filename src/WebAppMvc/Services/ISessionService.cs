using System;
using Core.DTOs;

namespace WebAppMvc.Services;

public interface ISessionService
{

    UserDTO? GetLoggedInUser(HttpContext context);

}
