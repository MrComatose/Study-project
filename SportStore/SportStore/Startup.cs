
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace SportStore
{

    public class Startup
    {

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDataBaseContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddDbContext<AppIdentityDataBaseContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                 .AddEntityFrameworkStores<AppIdentityDataBaseContext>()
                 .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrdersRepository, EFOrderRepository>();
            services.AddScoped<Cart>(sp=>SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory logger)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();

            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Product", action = "List" }
                      );
                routes.MapRoute(
                name: null,
                template: "Page{page:int}",
                defaults: new { controller = "Product", action = "List", page = 1 }
    
                      );
                routes.MapRoute(
            name: null,
            template: "{category}",
            defaults: new { controller = "Product", action = "List", page = 1 }

              );
            routes.MapRoute(
        name: null,
        template: "",
        defaults: new { controller = "Product", action = "List", page = 1 });
        routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

             
            });
            //SeedData.EnsurePopulated(app);
           //IdentitySeedData.EnsurePopulated(app);
        }
    }
}
