using StoreFront2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFront2.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        public ActionResult Index()
        {
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();

                ViewBag.Message = "There are no item in your cart.";
            }
            else
            {
                ViewBag.Message = null; 
            }
            return View(shoppingCart);
        }

        public ActionResult RemoveFromCart(int id)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["Cart"];

            shoppingCart.Remove(id);

            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }

        public ActionResult UpdateCart(int productID, int qty)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["Cart"];

            shoppingCart[productID].Qty = qty;

            Session["cart"] = shoppingCart;

            return RedirectToAction("Index");
        }
    }
}