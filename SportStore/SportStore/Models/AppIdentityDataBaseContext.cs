using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace SportStore.Models
{
    public class AppIdentityDataBaseContext:IdentityDbContext<IdentityUser>
    {
        public AppIdentityDataBaseContext(DbContextOptions<AppIdentityDataBaseContext> options):base(options)
        {

        }
    }
}
