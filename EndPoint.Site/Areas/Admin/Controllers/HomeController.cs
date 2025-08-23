using EndPoint.Site.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [RolesExceptCustomerAttribute]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}
