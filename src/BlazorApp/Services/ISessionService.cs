using System;
using Core.DTOs;

namespace BlazorApp.Services;

public interface ISessionService
{

    CustomerDTO? CurrentCustomer { get; set; }

}
