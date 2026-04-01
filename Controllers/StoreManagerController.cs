using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private MusicStoreEntities dbContext = new MusicStoreEntities();

        // GET: StoreManager
        public ActionResult Index()
        {
            var albums = dbContext.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        // GET: StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }

            Album album = dbContext.Albums.Include(a => a.Artist).Include(a => a.Genre).Single(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(dbContext.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(dbContext.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                dbContext.Albums.Add(album);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Album album = dbContext.Albums.Find(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewBag.ArtistId = new SelectList(dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(album).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(dbContext.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(dbContext.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(400);
            }
            Album album = dbContext.Albums.Include(a => a.Artist).Include(a => a.Genre).Single(a => a.AlbumId == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind("AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            dbContext.Entry(album).State = EntityState.Deleted;
            dbContext.SaveChanges();

            return RedirectToAction("Index");
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
