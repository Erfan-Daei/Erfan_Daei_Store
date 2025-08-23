using Endpoint.Api.Areas.Admin.Model.ProductManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.ProductManagement
{
    [Route("api/Area/Admin/ProductManager/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "ProductManagementAdmins")]
    [ApiController]
    public class ReviewManagerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        private readonly IReadToken _readToken;
        public ReviewManagerController(IProductFacad productFacad, IReadToken readToken)
        {
            _productFacad = productFacad;
            _readToken = readToken;
        }

        [HttpGet("Id")]
        public IActionResult GET(long Id)
        {
            var Result = _productFacad.GetAllReviewsService.Execute(Id);

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                ProductRivewList = Result.Data,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "ReviewManagerController", new { Area = "Admin", Id = "ReviewId" }, Request.Scheme) ?? "",
                        Rel = "ReplyToReview",
                        Method = "POST"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] ReplyToReviewDto _Request)
        {
            var AdminId = _readToken.GetUserId(User);
            var Result = _productFacad.AddReplyToReviewService.Execute(_Request.ReviewId, AdminId, _Request.ReplyDetail);

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "ReviewManagerController", new { Area = "Admin", Id = _Request.ReviewId }, Request.Scheme) ?? "",
                        Rel = "ViewReview",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
