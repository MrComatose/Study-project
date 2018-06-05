using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SportStore.Models
{
    public class EFOrderRepository : IOrdersRepository
    {
        private ApplicationDataBaseContext context;

        public EFOrderRepository(ApplicationDataBaseContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Product);

        public void DeleteOrder(int productId)
        {
            context.Orders.RemoveRange(Orders.Where(x=>x.Lines.OrderBy(y=>y.Product.ProductID==productId).Count()!=0));
            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}

