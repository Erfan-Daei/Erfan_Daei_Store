using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Notfound()
        {
            return View();
        }
    }
}
