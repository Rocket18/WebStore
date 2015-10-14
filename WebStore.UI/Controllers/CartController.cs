using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Models.Abstract;
using WebStore.Models.Entities;
using WebStore.UI.Models;


namespace WebStore.UI.Controllers
{
     [Authorize(Roles = "Client,Admin")]
    public class CartController : Controller
    {
        private IUnitOfWork repository;

        public CartController(IUnitOfWork cartRepository)
        {
            repository = cartRepository;
        }
        [HttpPost]
        public RedirectToRouteResult AddToCart(Cart cart, int productID, string returnUrl, int quantity = 1)
        {
            Products product = repository.Products.FindById(productID);
            if(product==null)
                throw new HttpException(404, "Not found");
            if (product != null)
            {
                cart.AddItem(product, quantity);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        [HttpPost]
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productID, string returnUrl)
        {
            Products product = repository.Products.FindById(productID);
            if (product == null)
                throw new HttpException(404, "Not found");
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
     
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }


    }
}
