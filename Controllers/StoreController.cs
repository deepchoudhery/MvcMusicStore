using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public StoreController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /Store/
        public ActionResult Index()
        {
            var genres = _dbContext.Genres.ToList();
            return View(genres);
        }

        // GET: /Store/Browse?genre=Disco
        public ActionResult Browse(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest();
            }

            var genreModel = _dbContext.Genres.Include(g => g.Albums).SingleOrDefault(g => g.Name == genre);
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
                return BadRequest();
            }

            var album = _dbContext.Albums.Include(a => a.Artist).Include(a => a.Genre).SingleOrDefault(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // GET: /Store/GenreMenu
        public ActionResult GenreMenu()
        {
            var genres = _dbContext.Genres.ToList();
            return PartialView(genres);
        }
    }
}
