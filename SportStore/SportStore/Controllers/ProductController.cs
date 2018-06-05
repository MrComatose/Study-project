
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {

        private IProductRepository repository;
        public ProductController(IProductRepository rep)
        {
            repository = rep;
        }
        public int PageSize = 4;
        public IActionResult List(string category=null,int page = 1)
            => View(new ProductListVIewModel()
            {
                Products = (repository.Products as IEnumerable<Product>).Where(p=> category==null||p.Category==category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),

                Page_info=new PagingInfo {
                    CurrentPage = page,
                    ItemsPerPage=PageSize,
                    TotalItems=repository.Products.Where(p => category == null || p.Category == category).Count()

                },
                CurrentCategory=category
            }
            ); 
        
    }
}