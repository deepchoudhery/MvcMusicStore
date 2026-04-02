using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public HomeController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Home/
        public IActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        // GET: /Home/About
        public IActionResult About()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        // GET: /Home/Contact
        public IActionResult Contact()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            return _dbContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count)
                .Take(count)
                .ToList();
        }
    }
}
