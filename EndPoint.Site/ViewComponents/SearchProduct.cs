using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class SearchProduct : ViewComponent
    {
        private readonly ILandingPageFacad _landingPageFacad;
        public SearchProduct(ILandingPageFacad landingPageFacad)
        {
            _landingPageFacad = landingPageFacad;
        }

        public IViewComponentResult Invoke()
        {
            var ParentCategories = _landingPageFacad.GetProductMenuService.Execute().Data;
            return View(viewName: "SearchProduct", ParentCategories);
        }
    }
}
