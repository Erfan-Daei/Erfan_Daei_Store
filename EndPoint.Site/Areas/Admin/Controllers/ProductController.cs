using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Commands.AddProduct;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Admin;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,ProductManagement_Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _productFacad;
        private readonly IHostingEnvironment _environment;
        public ProductController(IProductFacad productFacad, IHostingEnvironment hostEnvironment)
        {
            _productFacad = productFacad;
            _environment = hostEnvironment;
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
            List<IFormFile> Images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Images.Add(file);
            }
            _Request.Images = Images;
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
            List<IFormFile> Images = new List<IFormFile>();
            _Request.ImageSrc = Srcs;
            foreach (var image in _Request.ImageSrc)
            {
                if (image.Src.Contains(@"images\ProductImages\"))
                {
                    IFormFile FormFile = CreateIFormFile(_environment.WebRootPath + image.Src);
                    Images.Add(FormFile);
                }
            }
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Images.Add(file);
            }
            _Request.Images = Images;
            _Request.Sizes = _Sizes;
            return Json(_productFacad.EditProductService.Execute(_Request));
        }

        private IFormFile CreateIFormFile(string path)
        {
            var fileName = Path.GetFileName(path);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            fileStream.Close();
            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, memoryStream.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
        }

        [HttpGet]
        public IActionResult GetAllReviews(long Id)
        {
            return View(_productFacad.GetAllReviewsService.Execute(Id).Data);
        }

        [HttpPost]
        public IActionResult AddReplyToReview(long ReviewId, string ReplyDetail)
        {
            string UserId = ClaimUtility.GetUserId(User);
            return Json(_productFacad.AddReplyToReviewService.Execute(ReviewId, UserId, ReplyDetail));
        }
    }
}
