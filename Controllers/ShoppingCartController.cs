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

        //
        // GET: /ShoppingCart/
        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext, _dbContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        public IActionResult AddToCart(int id)
        {
            var addedAlbum = _dbContext.Albums
                .Single(album => album.AlbumId == id);

            var cart = ShoppingCart.GetCart(HttpContext, _dbContext);
            cart.AddToCart(addedAlbum);

            return RedirectToAction("Index");
        }

        // Remove the item from the cart
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(HttpContext, _dbContext);

            string albumName = _dbContext.Carts
                .Single(item => item.RecordId == id).Album!.Title;

            int itemCount = cart.RemoveFromCart(id);

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
    }
}
