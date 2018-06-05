using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
namespace SportStore.Models
{
    public class ApplicationDataBaseContext:DbContext
    {
        public ApplicationDataBaseContext (DbContextOptions<ApplicationDataBaseContext> options):base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
    public class ApplicationDataBaseFactory 
        : IDesignTimeDbContextFactory<ApplicationDataBaseContext>
    {
        public ApplicationDataBaseContext CreateDbContext(string[] args)
      => Program.BuildWebHost(args).Services
                 .GetRequiredService<ApplicationDataBaseContext>();
        
    }
}

