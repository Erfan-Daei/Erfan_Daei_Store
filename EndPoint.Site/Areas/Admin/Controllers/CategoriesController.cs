using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Commands.AddCategory;
using Practice_Store.Application.Services.Products.Commands.EditCategory;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,ProductManagement_Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult Index(long? ParentId)
        {
            return View(_productFacad.GetCategoriesService.Execute(ParentId).Data);
        }

        [HttpGet]
        public IActionResult AddCategory(long? ParentId)
        {
            ViewBag.ParentId = ParentId;
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(RequestAddCategoryDto _Request)
        {
            return Json(_productFacad.AddCategoryService.Execute(_Request));
        }

        [HttpPatch]
        public IActionResult EditCategory(RequestEditCategoryDto _Request) 
        {
            return Json(_productFacad.EditCategoryService.Execute(_Request));
        }

        [HttpDelete]
        public IActionResult DeleteCategory(long Id)
        {
            return Json(_productFacad.DeleteCategoryService.Execute(Id));
        }
    }
}
