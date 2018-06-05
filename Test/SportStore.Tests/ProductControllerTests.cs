
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {


        [Fact]
        public void Can_Paginate()
        {//opganizacia
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(((IEnumerable<Product>)(new Product[] {
                new Product{ProductID = 1,Name="P1" },
                new Product{ProductID=2,Name="P2" },
                new Product{ProductID=3,Name="P3" },
                    new Product{ProductID=4, Name="P4"},
                new Product{ProductID=5, Name="P5"}

            })).AsQueryable() /*as IQueryable<Product>*/);
            ProductController curent = new ProductController(mock.Object);
            curent.PageSize = 3;


            ProductListVIewModel result = (curent.List(null, 2) as ViewResult).ViewData.Model as ProductListVIewModel;
            Product[] array = result.Products.ToArray<Product>();
            Assert.True(array.Length == 2);
            Assert.Equal("P4", array[0].Name);
            Assert.Equal("P5", array[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                    new Product{ ProductID=1,Name="P1"},
                    new Product{ ProductID=2,Name="P2"},
                    new Product{ ProductID=3,Name="P3"},
                    new Product{ ProductID=4,Name="P4"},
                    new Product{ ProductID=5,Name="P5"},
            }).AsQueryable()
            );

            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };
            ProductListVIewModel result = ((ViewResult)controller.List(null, 2)).ViewData.Model as ProductListVIewModel;
            PagingInfo pageinfo = result.Page_info;
            Assert.Equal(2, pageinfo.CurrentPage);
            Assert.Equal(3, pageinfo.ItemsPerPage);
            Assert.Equal(5, pageinfo.TotalItems);
            Assert.Equal(2, pageinfo.TotalPages);


        }
        [Fact]
        public void Can_Filter_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
               (new Product[]
                {
                    new Product{ProductID=1,Name="P1",Category="Cat1" },
                    new Product{ProductID=2,Name="P2",Category="Cat2" },
                    new Product{ProductID=3,Name="P3",Category="Cat1" },
                    new Product{ProductID=4,Name="P4",Category="Cat2" },
                    new Product{ProductID=5,Name="P5",Category="Cat3" }
                }).AsQueryable()

                );

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;


            Product[] result = ((controller.List("Cat2",1) as ViewResult)
                .ViewData.Model as ProductListVIewModel)
                .Products.ToArray();
            Assert.True(result[0].Name == "P2");
            Assert.True(result[1].Name == "P4");
            Assert.Equal(2,result.Length);
     
        }

    }
}
