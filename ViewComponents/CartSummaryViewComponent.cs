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
            var cart = ShoppingCart.GetCart(_dbContext, HttpContext.Session, HttpContext.User.Identity?.Name);
            ViewData["CartCount"] = cart.GetCount();
            return View();
        }
    }
}
