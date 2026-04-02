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

        //
        // GET: /Store/
        public async Task<IActionResult> Index()
        {
            var genres = await _dbContext.Genres.ToListAsync();
            return View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco
        public async Task<IActionResult> Browse(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest();
            }

            var genreModel = await _dbContext.Genres
                .Include(g => g.Albums)
                .SingleOrDefaultAsync(g => g.Name == genre);

            if (genreModel == null)
            {
                return NotFound();
            }

            return View(genreModel);
        }

        //
        // GET: /Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var album = await _dbContext.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefaultAsync(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }
    }
}
