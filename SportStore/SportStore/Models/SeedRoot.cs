using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDataBaseContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDataBaseContext>();


           
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Price = 32.23M,
                        Description = "A boat fot one person.",
                        Category = "Water sport."

                    },
                    new Product
                    {
                        Name = "Boxing glowes",
                        Price = 23.42M,
                        Description = "Glowes from natural skin.",
                        Category = "Boxing."

                    },
                    new Product
                    {
                        Name = "Lifejucket",
                        Price = 21.32M,
                        Description = "Protective and fashionable.",
                        Category = "Water sport."
                    },
                    new Product
                    {
                        Name = "Soccet ball",
                        Price = 10.21M,
                        Description = "FIFA=approved size and weight.",
                        Category = "Football."
                    }
 );
                context.SaveChanges();

            }
        }
    }
}
