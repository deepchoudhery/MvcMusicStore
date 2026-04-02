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

        //
        // GET: /Checkout/AddressAndPayment
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddressAndPayment(Order order, [FromForm] string PromoCode)
        {
            if (!ModelState.IsValid)
            {
                return View(order);
            }

            if (!string.Equals(PromoCode, CheckoutController.PromoCode, StringComparison.OrdinalIgnoreCase))
            {
                return View(order);
            }

            order.Username = User.Identity!.Name;
            order.OrderDate = DateTime.Now;

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            var cart = ShoppingCart.GetCart(HttpContext, _dbContext);
            cart.CreateOrder(order);

            return RedirectToAction("Complete", new { id = order.OrderId });
        }

        //
        // GET: /Checkout/Complete
        public IActionResult Complete(int id)
        {
            bool isValid = _dbContext.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity!.Name);

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
