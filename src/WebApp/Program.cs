
using System.Text.Json;
using Core.DTOs;
using Data.EF;
using Data.Interfaces;
using Data.Services;
using Logic.Interfaces;
using Logic.Managers;
using Microsoft.EntityFrameworkCore;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


//add services to container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WebshopContext>(options =>
    options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString)));


//Add ProductDatabaseServiceEF
builder.Services.AddTransient<ICatalogusManager, CatalogusManagerEF>();
builder.Services.AddTransient<ICategoryManager, CategoryManagerEF>();
builder.Services.AddTransient<ILoginManager, LoginManagerEF>();
builder.Services.AddTransient<ICustomerManager, CustomerManagerEF>();
builder.Services.AddTransient<ISessionService, SessionService>();

// Services toevoegen
builder.Services.AddDistributedMemoryCache(); // Voor session storage in geheugen
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Hoe lang een sessie geldig blijft
});





//build container
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();  // ✅ Zorg dat deze altijd vroeg in de pipeline staat
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();





app.Use(async (context, next) =>
{

    // Controleer of de request voor een statisch bestand is, anders laadt de css niet voor de login page.
    if (context.Request.Path.StartsWithSegments("/css") ||
        context.Request.Path.StartsWithSegments("/js") ||
        context.Request.Path.StartsWithSegments("/images") ||
        context.Request.Path.StartsWithSegments("/lib"))
    {
        await next();
        return; // stopt de verdere middleware
    }

    //Login en logout page beide zonder sessiecontrole. Login hoeft gebruiker niet ingelogd te zijn en logout wordt doorgestuurd naar logout page waar de session gewist wordt en wordt doorgestuurd naar de index.
    if (context.Request.Path == "/Login" || context.Request.Path == "/Logout")
    {
        await next();
        return;
    }


    var UserNameLogin = context.Session.GetString("username");

    // controleer of gebruiker is ingelogd, zo niet dan naar login page
    if (string.IsNullOrEmpty(UserNameLogin))
    {
        context.Response.Redirect("/Login");
        return;
    }

    var LoginManager = context.RequestServices.GetService<ILoginManager>();

    if (LoginManager == null)
    {
        //Loginmanager niet beschikbaar?
        context.Response.Redirect("/Login");
        return;
    }

    var user = LoginManager.UserLogin(UserNameLogin);

    if (user == null)
    {
        //gebruiker onbekend
        context.Session.SetString("LoginError", "Gebruiker niet gevonden. Probeer opnieuw.");
        context.Response.Redirect("/Login");
        return;
    }



    // Json data meegeven aan session, zodat deze weer omgezet kan worden naar UserDTO in inde
    string userJson = JsonSerializer.Serialize(user);

    context.Session.SetString("user", userJson);


    await next();
});




app.Run();
