using Endpoint.Api.Model.ProductManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.JWTToken;
using Practice_Store.Application.Services.Products.Commands.AddReview;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.ProductManagement
{
    [Route("api/ProductManager/[controller]")]
    [ApiController]
    public class ReviewManagerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        private readonly IReadToken _readToken;
        public ReviewManagerController(IProductFacad productFacadc, IReadToken readToken)
        {
            _productFacad = productFacadc;
            _readToken = readToken;
        }

        [HttpGet("Id")]
        public IActionResult GET([FromQuery] long ProductId)
        {
            var Result = _productFacad.GetAllReviewsService.Execute(ProductId);

            dynamic Output = new
            {
                ReviewList = Result.Data,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", Request.Scheme) ?? "",
                        Rel = "ProductList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new {ProductId = ProductId}, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "ReviewManagerManager", Request.Scheme) ?? "",
                        Rel = "AddReview",
                        Method = "POST"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPost]
        [Authorize(Policy = "Customer&Admin")]
        public IActionResult POST([FromBody] AddReviewDto _Request)
        {
            var UserId = _readToken.GetUserId(User);
            var Result = _productFacad.AddReviewService.Execute(new RequestAddReview
            {
                ProductId = _Request.ProductId,
                UserId = UserId,
                ReviewDetail = _Request.ReviewDetail,
                Score = _Request.Score,
            });

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
                        Href = Url.Action("GET", "ProductManager", Request.Scheme) ?? "",
                        Rel = "ProductList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new {ProductId = _Request.ProductId}, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "ReviewManagerManager", new {ProductId = _Request.ProductId}, Request.Scheme) ?? "",
                        Rel = "ReviewList",
                        Method = "GET"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
