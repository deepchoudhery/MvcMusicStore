using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public HomeController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        //
        // GET: /Home/
        public async Task<IActionResult> Index()
        {
            var albums = await GetTopSellingAlbumsAsync(5);
            return View(albums);
        }

        //
        // GET: /Home/About
        public IActionResult About()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        //
        // GET: /Home/Contact
        public IActionResult Contact()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        private async Task<List<Album>> GetTopSellingAlbumsAsync(int count)
        {
            return await _dbContext.Albums
                .OrderByDescending(a => a.OrderDetails!.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}
