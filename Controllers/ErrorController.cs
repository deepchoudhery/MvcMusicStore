using Microsoft.AspNetCore.Mvc;

namespace MvcMusicStore.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /Error/
        public ActionResult Index()
        {
            return View("Error");
        }

        // GET: /Error/NotFound
        [ActionName("NotFound")]
        public ActionResult PageNotFound()
        {
            return View("NotFound");
        }
    }
}
