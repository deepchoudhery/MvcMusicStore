using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Error/PageNotFound
        public ActionResult PageNotFound()
        {
            return View("NotFound");
        }
    }
}
