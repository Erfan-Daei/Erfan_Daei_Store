using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Commands.AddReview;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;

namespace EndPoint.Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult GetProductList(string SearchKey, Ordering ordering = (Ordering)0, int CategoryId = 0, int Page = 1, int PageSize = 20)
        {
            return View(_productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
            {
                SearchKey = SearchKey,
                Page = Page,
                CategoryId = CategoryId,
            }, ordering, PageSize
            ).Data
            );
        }

        [HttpGet]
        public IActionResult GetProductDetail(long Id)
        {
            return View(_productFacad.GetProductDetails_SiteService.Execute(Id).Data);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        public IActionResult AddUserReview(RequestAddReview Request)
        {
            Request.UserId = ClaimUtility.GetUserId(User);
            return Json(_productFacad.AddReviewService.Execute(Request));
        }

        [HttpGet]
        public IActionResult GetAllReviews(long ProductId)
        {
            return View(_productFacad.GetAllReviewsService.Execute(ProductId).Data);
        }
    }
}
