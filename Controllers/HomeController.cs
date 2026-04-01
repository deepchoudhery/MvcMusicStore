using MvcMusicStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            return View(albums);            
        }

        //
        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Music Store Web App.";

            return View();
        }

        //
        // GET: /Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Music Store Web App.";

            return View();
        }

        // Return the top rated selling Albums
        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count
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