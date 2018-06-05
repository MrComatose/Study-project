using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;


namespace SportStore.Component
{
    public class CartSummaryViewComponent :ViewComponent
    {
        Cart cart;
        public CartSummaryViewComponent(Cart cartservices ) { cart = cartservices; }
        public IViewComponentResult Invoke()
        => View(cart);
    }
}
