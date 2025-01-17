using System;
using System.Security.Cryptography.X509Certificates;
using Core.Interfaces;
using Data.Services;
using Logic.Interfaces;
using Logic.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

class Program
{
    private readonly UI.UI _ui;
    public Program(UI.UI uI)
    {
        _ui = uI;
    }
    static async Task Main(string[] args)
    {


        //Host beheert de scope van je application en DI container. Maakt het eenvoudiger om DI-container op te zetten. 
        // Gebruiken voor registeren van services
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        //specifies the contract for a collection of services.
        var serviceCollection = builder.Services;
        ConfigureServices(serviceCollection);

        //bouwt een host object, deze bevat de DI-container. using zorgt ervoor dat na gebruik weer wordt opgeruimd, dus memory vrij gegeven.
        using IHost host = builder.Build();

        // haalt een instantie van Program uit de DI-container. en zorgt ervoor dat alle dependencies automatisch worden injected.
        var program = host.Services.GetRequiredService<Program>();

        await program.StartWebshop();

        //start host zolang als de applicatie runt. voor achtergrond taken.
        await host.RunAsync();


    }

    private static void ConfigureServices(IServiceCollection serviceCollection)
    {

        // add services to serviceCollection 
        serviceCollection.AddTransient<ICartDatabaseService, CartDatabaseService>();
        serviceCollection.AddTransient<ICategoryDatabaseService, CategoryDatabaseService>();
        serviceCollection.AddTransient<IProductDatabaseService, ProductDatabaseService>();
        serviceCollection.AddTransient<ICustomerDatabaseService, CustomerDatabaseService>();
        serviceCollection.AddTransient<ILoginService, LoginService>();
        serviceCollection.AddTransient<IDatabaseService, DatabaseService>();
        serviceCollection.AddTransient<IOrderDatabaseService, OrderDatabaseService>();
        serviceCollection.AddTransient<IOrderItemDatabaseService, OrderItemDatabaseService>();


        // add managers to serviceCollection 
        serviceCollection.AddTransient<IShoppingCart, ShoppingCart>();
        serviceCollection.AddTransient<ICategoryManager, CategoryManager>();
        serviceCollection.AddTransient<IOrderManager, OrderManager>();
        serviceCollection.AddTransient<ICatalogusManager, CatalogusManager>();
        serviceCollection.AddTransient<ILoginManager, LoginManager>();
        serviceCollection.AddTransient<UI.UI>();
        serviceCollection.AddTransient<Program>();
        serviceCollection.AddTransient<Helpers.Presenter>();

    }


    public async Task StartWebshop()
    {
        try
        {
            await _ui.StartWebshop();

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Er is een fout opgetreden tijdens het starten van de webshop: {ex.Message}");
        }

    }
}
