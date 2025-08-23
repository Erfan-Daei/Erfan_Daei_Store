using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Domain.Entities.LandingPage;

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
        public IActionResult AddLandingPageImage(string Title, string Link, LandingPageImageLocation ImageLocation)
        {
            IFormFile Image = null;
            if (Request.Form.Files.Count >= 1)
            {
                Image = Request.Form.Files[0];
            }
            return Json(_landingPageFacad.AddImage_LandingPageService.Execute(new RequestAddImage_LandingPageDto
            {
                Image = Image,
                Title = Title,
                Link = Link,
                ImageLocation = ImageLocation
            }));
        }

        [HttpPut]
        public IActionResult EditLandingPageImage(RequestEditImage_LandingPageDto _Request)
        {
            IFormFile Image = null;
            if (Request.Form.Files.Count > 0)
            {
                Image = Request.Form.Files[0];
            }
            return Json(_landingPageFacad.EditImageService.Execute(new RequestEditImage_LandingPageDto
            {
                Id = _Request.Id,
                Image = Image,
                Title = _Request.Title,
                Link = _Request.Link,
                ImageLocation = _Request.ImageLocation
            }));
        }

        [HttpDelete]
        public IActionResult DeleteLandingPageImage(long Id)
        {
            return Json(_landingPageFacad.DeleteImage_LandingPageService.Execute(Id));
        }
    }
}
