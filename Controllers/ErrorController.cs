using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Error/NotFound
        [ActionName("NotFound")]
        public IActionResult PageNotFound()
        {
            return View("NotFound");
        }
    }
}
