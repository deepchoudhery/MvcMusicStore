using Microsoft.AspNetCore.Mvc;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index() { return View(); }

        public new ActionResult NotFound() { return View(); }
    }
}