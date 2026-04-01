using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();
        const string PromoCode = "FREE";

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(Microsoft.AspNetCore.Http.IFormCollection values)
        {
            var order = new Order();
            TryUpdateModelAsync(order).GetAwaiter().GetResult();

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode, StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    dbContext.Orders.Add(order);
                    dbContext.SaveChanges();

                    var cart = ShoppingCart.GetCart(HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete", new { id = order.OrderId });
                }
            }
            catch
            {
                return View(order);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            bool isValid = dbContext.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
