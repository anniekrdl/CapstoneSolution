using Microsoft.EntityFrameworkCore;

namespace Data.EF;

public class WebshopContext : DbContext
{
    public WebshopContext(DbContextOptions<WebshopContext> options) : base(options)
    {

    }
}