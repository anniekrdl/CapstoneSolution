using System;
using Core.DTOs;

namespace BlazorAppWeb.Services;

public class SessionService : ISessionService
{
    private CustomerDTO? _currentCustomer;
    public CustomerDTO? CurrentCustomer
    {
        get => _currentCustomer;
        set
        {
            Console.WriteLine($"SessionService: CurrentCustomer set to {value?.Name}");
            _currentCustomer = value;
        }
    }
}