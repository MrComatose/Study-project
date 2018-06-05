using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public interface IOrdersRepository
    {
        IQueryable<Order> Orders { get;  }
        void SaveOrder(Order order);
        void DeleteOrder(int productId);
    }
}
