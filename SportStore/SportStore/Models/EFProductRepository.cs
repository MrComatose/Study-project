using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private ApplicationDataBaseContext context;
        public EFProductRepository(ApplicationDataBaseContext db ) { context = db; }
        public IQueryable<Product> Products => context.Products;

        public Product DeleteProducts(int productID)
        {
            Product dbentry = context.Products.FirstOrDefault(x => x.ProductID == productID);
            if (dbentry!=null)
            {
              
                context.Products.Remove(dbentry);
                context.SaveChanges();
            }
            return dbentry;
        }

        public void SaveProducts(Product product)
        {
            if (product.ProductID==0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbentry = context.Products.FirstOrDefault(x=>x.ProductID==product.ProductID);
                if (dbentry!=null)
                {
                    dbentry.Name = product.Name;
                    dbentry.Price = product.Price;
                    dbentry.Category = product.Category;
                    dbentry.Description = product.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
