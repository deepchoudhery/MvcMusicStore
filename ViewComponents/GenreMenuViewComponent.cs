using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.ViewComponents
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly MusicStoreEntities _dbContext;

        public GenreMenuViewComponent(MusicStoreEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var genres = _dbContext.Genres.ToList();
            return View(genres);
        }
    }
}
