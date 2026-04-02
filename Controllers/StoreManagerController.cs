using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly MusicStoreEntities _dbContext;

        public StoreManagerController(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: StoreManager
        public IActionResult Index()
        {
            var albums = _dbContext.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre);
            return View(albums.ToList());
        }

        // GET: StoreManager/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Album? album = _dbContext.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public IActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Albums.Add(album);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Album? album = _dbContext.Albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(album).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(_dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(_dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Album? album = _dbContext.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a => a.AlbumId == id);

            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Album? album = _dbContext.Albums.Find(id);
            if (album != null)
            {
                _dbContext.Albums.Remove(album);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
