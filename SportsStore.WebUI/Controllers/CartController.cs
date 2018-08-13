using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductsRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductsRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart , string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = this.GetCart(),ReturnUrl = returnUrl
               // Cart = cart,
               // ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //-----------------------------------
         private Cart GetCart()
          {
              //Creates a session[] array of type Cart
              //Gets Http session object from current HTTP request
              //and casts this session to the type of Cart.
              Cart cart = (Cart)Session["Cart"];

              //checks if cart is null
              if (cart == null)
              {
                  //if cart local object is null
                  //try to create a newly cart based on it's http:// session info
                  cart = new Cart();
                  Session["Cart"] = cart;
              }
              return cart;
          }
        //-------------------------------------

        //public ViewResult Summary(Cart cart) //chto to tut..okseju..ya sveru so svoim kodom
        //{
        //    return View(cart);
        //}
         public PartialViewResult Summary(Cart cart)
         {
            //возвращает частичную (мини страничку) всунутую в общий контекст сайта
             //по этому она должна быть частичной 
             return PartialView(cart);
         }



        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) // Метод Checkout возвращает представление по умолчанию и передает в него новый объект ShippingDetails как модель представления
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

    }
}
