using System;
using Core.DTOs;

namespace BlazorApp.Services;

public class SessionService : ISessionService
{
    public CustomerDTO? CurrentCustomer { get; set; }
}
