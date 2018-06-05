using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        IOrdersRepository Orders;
        Cart cart;
        public OrderController(IOrdersRepository repository,Cart cartService)
        {
            Orders = repository;
            cart = cartService;
        }
        public IActionResult Checkout()
        {
            return View(new Order());
        }
        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (cart.Lines.Count()==0)
            {
                ModelState.AddModelError("","SOrry your cart is empty.");

            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                Orders.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
        [Authorize]
        public ViewResult List() => View(Orders.Orders/*.Where(x => !x.Shipped)*/);
        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int OrderID)
        {
            Order some = Orders.Orders.FirstOrDefault(x => x.OrderID == OrderID);
            if (some!=null)
            {
                some.Shipped = true;
                Orders.SaveOrder(some);
            }
            return RedirectToAction(nameof(List));
        }
    }
}