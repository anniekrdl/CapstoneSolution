using System;
using System.Text.Json;
using Core.DTOs;

namespace WebAppMvc.Services;

public class SessionService : ISessionService
{

    public UserDTO? GetLoggedInUser(HttpContext context)
    {
        var userJson = context.Session.GetString("user");
        // naar een service

        if (!string.IsNullOrEmpty(userJson))
        {
            // Parse de JSON om de 'Role' eruit te halen
            using (JsonDocument doc = JsonDocument.Parse(userJson))
            {

                // Probeer de 'Role' op te halen en sla op in roleElement
                if (doc.RootElement.TryGetProperty("Role", out JsonElement roleElement))
                {
                    // Haal de waarde van de 'Role'
                    string role = roleElement.GetString() ?? "Customer";

                    if (role == "Administrator")
                    {
                        // if Administrator Deserialize als Administrator
                        return JsonSerializer.Deserialize<AdministratorDTO>(userJson);

                    }
                    else
                    {
                        // if Customer Deserialize als Customer
                        return JsonSerializer.Deserialize<CustomerDTO>(userJson);

                    }
                }
                else
                {
                    Console.WriteLine("Geen 'Role' gevonden in de JSON.");
                }
            }

        }
        else
        {
            Console.WriteLine("Geen user data in session");

        }
        return null;
    }
}
