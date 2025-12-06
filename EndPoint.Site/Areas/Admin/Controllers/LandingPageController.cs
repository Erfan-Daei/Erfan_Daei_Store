using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteManagement_Admin")]
    public class LandingPageController : Controller
    {
        private readonly ILandingPageFacad _landingPageFacad;
        public LandingPageController(ILandingPageFacad landingPageFacad)
        {
            _landingPageFacad = landingPageFacad;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_landingPageFacad.GetImages_SiteService.Execute().Data);
        }

        [HttpGet]
        public IActionResult AddLandingPageImage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLandingPageImage(RequestAddImage_LandingPageDto _Request)
        {
            return Json(_landingPageFacad.AddImage_LandingPageService.Execute(_Request));
        }

        [HttpPut]
        public IActionResult EditLandingPageImage(RequestEditImage_LandingPageDto _Request)
        {
            return Json(_landingPageFacad.EditImageService.Execute(_Request));
        }

        [HttpDelete]
        public IActionResult DeleteLandingPageImage(long Id)
        {
            return Json(_landingPageFacad.DeleteImage_LandingPageService.Execute(Id));
        }
    }
}
