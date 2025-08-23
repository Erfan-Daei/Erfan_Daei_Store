using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class SearchProduct : ViewComponent
    {
        IProductFacad _productFacad;
        public SearchProduct(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IViewComponentResult Invoke()
        {
            var ParentCategories = _productFacad.GetProductMenuService.Execute().Data;
            return View(viewName: "SearchProduct", ParentCategories);
        }
    }
}
