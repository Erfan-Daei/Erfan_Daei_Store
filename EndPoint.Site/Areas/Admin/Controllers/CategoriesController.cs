using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

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
        public IActionResult AddCategory(long? ParentId, string Name)
        {
            return Json(_productFacad.AddCategoryService.Execute(ParentId, Name));
        }

        [HttpPatch]
        public IActionResult EditCategory(long Id, string Name)
        {
            return Json(_productFacad.EditCategoryService.Execute(Id, Name));
        }

        [HttpDelete]
        public IActionResult DeleteCategory(long Id)
        {
            return Json(_productFacad.DeleteCategoryService.Execute(Id));
        }
    }
}
