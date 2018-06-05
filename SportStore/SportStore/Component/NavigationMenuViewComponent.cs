using Microsoft.AspNetCore.Mvc;

using System.Linq;

using SportStore.Models;

namespace SportStore.Component
{
    public class NavigationMenuViewComponent:ViewComponent
    {
        private IProductRepository Repository;
        public NavigationMenuViewComponent(IProductRepository rep)
        {
            Repository = rep;
        }
       
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(Repository.Products.Select(x=>x.Category).Distinct().OrderBy(x=>x));
        }
    }
}
