

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Conteins_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{Name="P1",ProductID=1 },
                new Product{Name="P2",ProductID=2 },
                new Product{Name="P3",ProductID=3 }
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object,null);

            Product[] results = GetViewModel<IQueryable<Product>>(controller.Index()).ToArray();
            Assert.Equal(3,results.Length);
                

        }
        private T GetViewModel<T>(IActionResult a)where T:class
        {
            return (a as ViewResult)?.ViewData.Model as T;
        }
        [Fact]
        public void Can_Save_Chenges()
        {
            Mock<IProductRepository> moq = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempdata = new Mock<ITempDataDictionary>();

            AdminController controller = new AdminController(moq.Object,null) { TempData=tempdata.Object};
            Product product = new Product { Name = "Test" };
            IActionResult result = controller.Edit(product);

            moq.Verify(m=>m.SaveProducts(product));
            Assert.IsType<RedirectToActionResult>(result);Assert.Equal("Index",(result as RedirectToActionResult).ActionName);


        }
    }
}