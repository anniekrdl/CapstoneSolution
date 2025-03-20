using Core.DTOs;

namespace BlazorAppWeb.Services;

public interface ISessionService
{

    CustomerDTO? CurrentCustomer { get; set; }

}
