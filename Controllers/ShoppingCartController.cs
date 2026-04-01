using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        //
        // GET: /ShoppingCart/

        public ActionResult Index()
        {
            //var cart = ShoppingCart.GetCart(this.HttpContext);
            var cart = ShoppingCart.GetCart(this);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            // Return the view
            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5

        public ActionResult AddToCart(int id)
        {

            // Retrieve the album from the database
            var addedAlbum = dbContext.Albums
                .Single(album => album.AlbumId == id);

            //var cart = ShoppingCart.GetCart(this.HttpContext);
            var cart = ShoppingCart.GetCart(this);

            // Add it to the shopping cart
            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        // Remove the item from the cart
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            
            //var cart = ShoppingCart.GetCart(this.HttpContext);
            var cart = ShoppingCart.GetCart(this);

            // Get the name of the album to display confirmation
            string albumName = dbContext.Carts
                .Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = System.Net.WebUtility.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary

        public ActionResult CartSummary()
        {
            //var cart = ShoppingCart.GetCart(this.HttpContext);
            var cart = ShoppingCart.GetCart(this);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartSummary");
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