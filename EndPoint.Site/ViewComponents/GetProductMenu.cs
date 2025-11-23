using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class GetProductMenu : ViewComponent
    {
        private readonly ILandingPageFacad _landingPageFacad;
        public GetProductMenu(ILandingPageFacad landingPageFacad)
        {
            _landingPageFacad = landingPageFacad;
        }

        public IViewComponentResult Invoke()
        {
            var ProductMenu = _landingPageFacad.GetProductMenuService.Execute().Data;
            return View(viewName: "GetProductMenu", ProductMenu);
        }
    }
}
