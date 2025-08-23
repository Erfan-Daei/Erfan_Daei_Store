using Endpoint.Api.Model.ProductManagement;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;
using Practice_Store.Common;

namespace Endpoint.Api.Controllers.ProductManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMangerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        public ProductMangerController(IProductFacad productFacadc)
        {
            _productFacad = productFacadc;
        }

        [HttpGet("Id")]
        public IActionResult GET([FromQuery] long ProductId)
        {
            var Result = _productFacad.GetProductDetails_SiteService.Execute(ProductId);

            dynamic Output = new
            {
                ProductDetail = Result.Data,
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
                        Href = Url.Action("GET", "ReviewManagerManager", new {ProductId = ProductId}, Request.Scheme) ?? "",
                        Rel = "ReviewList",
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

        [HttpGet]
        public IActionResult GET([FromQuery] GetProductListDto _Request)
        {
            var Result = _productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
            {
                SearchKey = _Request.SearchKey,
                CategoryId = _Request.CategoryId,
                Page = _Request.Page
            }, _Request.Ordering, _Request.PageSize);

            dynamic Output = new
            {
                ProductList = Result.Data.ProductList,
                CurrentPage = Result.Data.CurrentPage,
                PageSize = Result.Data.PageSize,
                RowsCount = Result.Data.RowsCount,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new { ProductId = "ProductId"}, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "ReviewManagerManager", new {ProductId = "ProductId"}, Request.Scheme) ?? "",
                        Rel = "ReviewList",
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
    }
}
