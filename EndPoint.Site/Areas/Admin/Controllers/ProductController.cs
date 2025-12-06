using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Commands.AddProduct;
using Practice_Store.Application.Services.Products.Commands.AddReplyToReview;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Admin;


namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,ProductManagement_Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllSubCategoriesService.Execute().Data, "Id", "Name", 0);
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(RequestAddProductDto _Request, List<ProductSizeDto> _Sizes)
        {
            _Request.Sizes = _Sizes;
            return Json(_productFacad.AddProductService.Execute(_Request));
        }

        [HttpGet]
        public IActionResult GetProductList(RequestGetProductList_AdminDto Request, int PageSize = 10)
        {
            return View(_productFacad.GetProductList_AdminService.Execute(new RequestGetProductList_AdminDto
            {
                SearchKey = Request.SearchKey,
                Page = Request.Page == 0 ? 1 : Request.Page,
                PageSize = PageSize
            }).Data);
        }

        [HttpDelete]
        public IActionResult DeleteProduct(long Id)
        {
            return Json(_productFacad.DeleteProductService.Execute(Id));
        }

        [HttpPatch]
        public IActionResult ChangeProductDisplayed(long Id)
        {
            return Json(_productFacad.ChangeProductDisplayedService.Execute(Id));
        }

        [HttpGet]
        public IActionResult GetProductDetails(long Id)
        {
            return View(_productFacad.GetProductDetails_AdminService.Execute(Id).Data);
        }

        [HttpGet]
        public IActionResult EditProduct(long Id)
        {
            var Result = _productFacad.GetProductDetails_AdminService.Execute(Id).Data;
            ViewBag.EditCategories = new SelectList(_productFacad.GetAllSubCategoriesService.Execute().Data, "Id", "Name", Result.CategoryId);
            return View(_productFacad.GetProductDetails_AdminService.Execute(Id).Data);
        }

        [HttpPut]
        public IActionResult EditProduct(RequestEditProductDto _Request, List<EditProductSizeDto> _Sizes, List<EditProductImageSrcDto> Srcs)
        {
            _Request.Sizes = _Sizes;
            _Request.ImageSrc = Srcs;
            return Json(_productFacad.EditProductService.Execute(_Request));
        }

        [HttpGet]
        public IActionResult GetAllReviews(long Id)
        {
            return View(_productFacad.GetAllReviewsService.Execute(Id).Data);
        }

        [HttpPost]
        public IActionResult AddReplyToReview(RequestAddReplyToReviewDto _Request)
        {
            string UserId = ClaimUtility.GetUserId(User);
            _Request.UserId = UserId;
            return Json(_productFacad.AddReplyToReviewService.Execute(_Request));
        }
    }
}
