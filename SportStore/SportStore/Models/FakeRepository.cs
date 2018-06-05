using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeRepository /*: IProductRepository*/
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product{Name="Football", Price=25},
            new Product{Name="SurfBoatd",Price=23},
            new Product{Name="Boxing Glowes",Price=23 }

        } as IQueryable<Product>;
    }
}
