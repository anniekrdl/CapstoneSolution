using BlazorApp;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseApiUrl = builder.HostEnvironment.IsDevelopment()
    ? "http://localhost:5080/"  // Development URL
    : "https://your-production-api-url";  // Production URL

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseApiUrl),
    DefaultRequestHeaders = { { "Accept", "application/json" } }
});

builder.Services.AddSingleton<ISessionService, SessionService>();

await builder.Build().RunAsync();
