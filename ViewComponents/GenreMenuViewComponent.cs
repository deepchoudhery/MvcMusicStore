using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.ViewComponents
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly MusicStoreEntities dbContext = new MusicStoreEntities();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = dbContext.Genres.ToList();
            return View(genres);
        }
    }
}
