using MvcMusicStore.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        // GET: /Home/
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            return View(albums);
        }

        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Music Store Web App.";
            return View();
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            return dbContext.Albums.OrderByDescending(a => a.OrderDetails.Count()).Take(count).ToList();
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