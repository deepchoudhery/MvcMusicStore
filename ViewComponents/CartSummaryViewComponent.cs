using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly MusicStoreEntities _dbContext;

        public CartSummaryViewComponent(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var cart = ShoppingCart.GetCart(HttpContext, _dbContext);
            ViewData["CartCount"] = cart.GetCount();
            return View();
        }
    }
}
