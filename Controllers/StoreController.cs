using MvcMusicStore.Models;
using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        // GET: /Store/
        public ActionResult Index()
        {
            var genres = dbContext.Genres.ToList();
            return View(genres);
        }

        // GET: /Store/Browse?genre=Disco
        public ActionResult Browse(string genre)
        {
            if (String.IsNullOrEmpty(genre))
            {
                return StatusCode(400);
            }

            var genreModel = dbContext.Genres.Include(g => g.Albums).Single(g => g.Name == genre);
            if (genreModel == null)
            {
                return NotFound();
            }
            return View(genreModel);
        }

        // GET: /Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }

            var album = dbContext.Albums.Include(a => a.Artist).Include(a => a.Genre).Single(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // GET: /Store/GenreMenu (partial, called via Html.RenderAction in layout)
        public ActionResult GenreMenu()
        {
            var genres = dbContext.Genres.ToList();
            return PartialView(genres);
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
