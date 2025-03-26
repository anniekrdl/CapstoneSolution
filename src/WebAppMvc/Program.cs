using System.Text.Json;
using Data.EF;
using Logic.Interfaces;
using Logic.Managers;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using WebAppMvc.Services;

var builder = WebApplication.CreateBuilder(args);

//add services to container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WebshopContext>(options =>
    options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/User/Login";  // Login URL
        options.AccessDeniedPath = "/AccessDenied";  // Toegang geweigerd URL
        options.Cookie.Name = "UserLoginCookie";  // Naam van de cookie
        options.SlidingExpiration = true;  // Cookies verlopen na een periode van inactiviteit
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Hoe lang de cookie geldig is
    });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Make the cookie HTTP-only
    options.Cookie.IsEssential = true; // Ensure the cookie is essential
});

//Add ProductDatabaseServiceEF
builder.Services.AddTransient<ICatalogusManager, CatalogusManagerEF>();
builder.Services.AddTransient<ICategoryManager, CategoryManagerEF>();
builder.Services.AddTransient<ILoginManager, LoginManagerEF>();
builder.Services.AddTransient<ICustomerManager, CustomerManagerEF>();
builder.Services.AddTransient<IOrderManager, OrderManagerEF>();
builder.Services.AddSingleton<ISessionService, SessionService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Voeg authenticatie en autorisatie toe
app.UseAuthentication();
app.UseAuthorization();

app.UseSession(); // Ensure session middleware is used

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
