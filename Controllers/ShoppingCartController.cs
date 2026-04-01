using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;
using MvcMusicStore.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public ShoppingCartController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            var addedAlbum = _dbContext.Albums.Single(album => album.AlbumId == id);
            var cart = ShoppingCart.GetCart(this);
            cart.AddToCart(addedAlbum);
            return RedirectToAction("Index");
        }

        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this);

            string albumName = _dbContext.Carts
                .Single(item => item.RecordId == id).Album!.Title ?? string.Empty;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = System.Net.WebUtility.HtmlEncode(albumName) + " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        // GET: /ShoppingCart/CartSummary
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
