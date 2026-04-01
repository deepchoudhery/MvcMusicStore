using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly MusicStoreEntities _dbContext;
        const string PromoCode = "FREE";

        public CheckoutController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(Order order)
        {
            try
            {
                string promoCode = Request.Form["PromoCode"]!;
                if (!string.Equals(promoCode, PromoCode, StringComparison.OrdinalIgnoreCase))
                {
                    return View(order);
                }

                if (!ModelState.IsValid)
                {
                    return View(order);
                }

                order.Username = User.Identity!.Name!;
                order.OrderDate = DateTime.Now;

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                var cart = ShoppingCart.GetCart(this);
                cart.CreateOrder(order);

                return RedirectToAction("Complete", new { id = order.OrderId });
            }
            catch
            {
                return View(order);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            bool isValid = _dbContext.Orders.Any(
                o => o.OrderId == id && o.Username == User.Identity!.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
