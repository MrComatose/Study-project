using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using Microsoft.AspNetCore.Authorization;
namespace SportStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository Repository;
        private IOrdersRepository OrderRepository;
            public AdminController(IProductRepository items, IOrdersRepository rep)
        {
            Repository = items;
            OrderRepository = rep;
        }
        public ViewResult Index()
        {
            return View(Repository.Products.AsEnumerable());
        }
        public ViewResult Edit(int productID) { return View(Repository.Products.FirstOrDefault(x=>x.ProductID==productID)); }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                Repository.SaveProducts(product);
                TempData["Message"] = $"{product.Name} has been saved.";
                return RedirectToAction("Index");
            }
            else return View(product);
        }
        public ViewResult Create() => View("Edit", new Product());
        [HttpPost]
        public IActionResult Delete(int productId)
        {
            OrderRepository.DeleteOrder(productId);
            Product Deleted = Repository.DeleteProducts(productId);
            if (Deleted!=null)
            {
                TempData["Message"] = $"{Deleted.Name} was deleted.";

            }
            return RedirectToAction("Index");
        }
    }
}