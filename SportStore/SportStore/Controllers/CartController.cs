using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using SportStore.Infrastructure;
namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        Cart cart;
     
        public CartController(IProductRepository rep,Cart cartservice)
        {
            repository = rep;
            cart = cartservice;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }
        public RedirectToActionResult AddToCart(int ProductId,string returnUrl,int Quent) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == ProductId);
            if (product!=null&&Quent>0&&Quent<50)
            {
             
                cart.AddItem(product, Quent);
          
               
            }
 
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId,string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p=>p.ProductID==productId);
            if (product!=null)
            {
               
                cart.RemoveLine(product);
               
            }
            return RedirectToAction("Index",new { returnUrl});
        }

    
    }
}