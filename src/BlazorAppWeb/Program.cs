using BlazorAppWeb.Components;
using BlazorAppWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var baseApiUrl = builder.Environment.IsDevelopment()
    ? "https://localhost:5080/"  // Development URL
    : "https://your-production-api-url";  // Production URL

// Configure HttpClient with BaseAddress
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseApiUrl),
    DefaultRequestHeaders = { { "Accept", "application/json" } }
});

builder.Services.AddSingleton<ISessionService, SessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
