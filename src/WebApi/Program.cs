using Data.EF;
using Logic.Interfaces;
using Logic.Managers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Voeg services toe aan de container
builder.Services.AddControllers();

// Add CORS voor BlazorApp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder
            .WithOrigins("https://localhost:5274") // Pas de URL aan naar de URL van je Blazor-app
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());  // Dit zorgt ervoor dat cookies worden meegestuurd);
});

//add services to container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
.Replace("{MyDatabasePassword}", builder.Configuration["ConnectionString:MyDatabasePassword"]);

builder.Services.AddDbContext<WebshopContext>(options =>
    options.UseMySql(connectionString, MySqlServerVersion.AutoDetect(connectionString)));

builder.Services.AddAuthentication("MyCookieAuth")
.AddCookie("MyCookieAuth", options =>
{
    options.LoginPath = "/login";  // Login URL
    options.AccessDeniedPath = "/AccessDenied";  // Toegang geweigerd URL
    options.Cookie.Name = "UserLoginCookie";  // Naam van de cookie
    options.SlidingExpiration = true;  // Cookies verlopen na een periode van inactiviteit
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Hoe lang de cookie geldig is
    options.Cookie.SameSite = SameSiteMode.None; // Zorgt dat cookies cross-origin werken
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Vereist HTTPS (voor productie)

});

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AuthorizedUser", policy =>
        policy.RequireAuthenticatedUser());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add ProductDatabaseServiceEF
builder.Services.AddTransient<ICatalogusManager, CatalogusManagerEF>();
builder.Services.AddTransient<ICategoryManager, CategoryManagerEF>();
builder.Services.AddTransient<ILoginManager, LoginManagerEF>();
builder.Services.AddTransient<ICustomerManager, CustomerManagerEF>();
builder.Services.AddTransient<IShoppingCart, ShoppingCartEF>();
builder.Services.AddTransient<IOrderManager, OrderManagerEF>();
//builder.Services.AddSingleton<ISessionService, SessionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
