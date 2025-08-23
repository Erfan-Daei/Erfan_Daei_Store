using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.Site.ViewComponents
{
    public class GetProductMenu : ViewComponent
    {
        private readonly IProductFacad _productFacad;
        public GetProductMenu(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        public IViewComponentResult Invoke()
        {
            var ProductMenu = _productFacad.GetProductMenuService.Execute().Data;
            return View(viewName: "GetProductMenu", ProductMenu);
        }
    }
}
