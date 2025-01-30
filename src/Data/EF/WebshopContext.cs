using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.EF;

public class WebshopContext : DbContext
{
    public WebshopContext(DbContextOptions<WebshopContext> options) : base(options)
    {

    }

    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    /*
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<ShoppingCartItemEntity> ShoppingCartItems { get; set; }
    */

}