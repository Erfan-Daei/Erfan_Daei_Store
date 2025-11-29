using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.SiteManagement
{
    [Route("api/Area/Admin/[controller]")]
    [Authorize(Policy = "SiteManagementAdmins")]
    [ApiController]
    public class LandingPageManagerController : ControllerBase
    {
        private readonly ILandingPageFacad _landingPageFacad;
        public LandingPageManagerController(ILandingPageFacad landingPageFacad)
        {
            _landingPageFacad = landingPageFacad;
        }

        [HttpGet]
        public IActionResult GET()
        {
            var Result = _landingPageFacad.GetImages_SiteService.Execute();

            dynamic Output = new
            {
                Images = Result.Data,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("POST", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "AddImage",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "EditImage",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "DeleteImage",
                        Method = "DELETE"
                    }
                }
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] RequestAddImage_LandingPageDto _Request)
        {
            var Result = _landingPageFacad.AddImage_LandingPageService.Execute(_Request);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "ImageList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "EditImage",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "DeleteImage",
                        Method = "DELETE"
                    }
                }
            };

            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] RequestEditImage_LandingPageDto _Request)
        {
            var Result = _landingPageFacad.EditImageService.Execute(_Request);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("POST", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "AddImage",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "ImageList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "LandingPageManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "DeleteImage",
                        Method = "DELETE"
                    }
                }
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE(long Id)
        {
            var Result = _landingPageFacad.DeleteImage_LandingPageService.Execute(Id);

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
            };

            return Ok(Output);
        }
    }
}
