
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using SportStore.Component;

namespace SportStore.Tests
{
    public  class NavigateMenuViewComponentTest
    {
        [Fact]
        public void Can_select_categoires()
        {
            Mock<IProductRepository> moq = new Mock<IProductRepository>();
            moq.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{Category="Box"},new Product{Category="Swimming"},new Product{Category="Aqua aerobica"},new Product{Category="Box"}
            }.AsQueryable()
            );
            NavigationMenuViewComponent Component = new NavigationMenuViewComponent(moq.Object);
            string[] result = ((IEnumerable<string>)(Component.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray() ;
            Assert.Equal(new string[] { "Aqua aerobica","Box", "Swimming"},result);
        }
        [Fact]
        public void Indicates_Category()
        {
            string categoryToSelect = "apple";
            Mock<IProductRepository> moq = new Mock<IProductRepository>();
            moq.Setup(x => x.Products).Returns(
                new Product[] { new Product { Category = "Apple" }, new Product { Category = "Oranges" } }.AsQueryable()
                );
            NavigationMenuViewComponent comp = new NavigationMenuViewComponent(moq.Object);
            comp.ViewComponentContext = new ViewComponentContext {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
                { RouteData = new Microsoft.AspNetCore.Routing.RouteData() } };
            comp.RouteData.Values["category"] = categoryToSelect;
            string result = (string)(comp.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];
            Assert.Equal(categoryToSelect,result);
        }
    }
}
