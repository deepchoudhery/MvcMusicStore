using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;
using System;

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
        public IActionResult Index()
        {
            var genres = _dbContext.Genres.ToList();
            return View(genres);
        }

        // GET: /Store/Browse?genre=Disco
        public IActionResult Browse(string genre)
        {
            if (string.IsNullOrEmpty(genre))
            {
                return BadRequest();
            }

            var genreModel = _dbContext.Genres
                .Include(g => g.Albums)
                .SingleOrDefault(g => g.Name == genre);

            if (genreModel == null)
            {
                return NotFound();
            }
            return View(genreModel);
        }

        // GET: /Store/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var album = _dbContext.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }
    }
}
