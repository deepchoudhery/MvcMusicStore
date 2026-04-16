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
        public new IActionResult NotFound()
        {
            return View();
        }
    }
}
