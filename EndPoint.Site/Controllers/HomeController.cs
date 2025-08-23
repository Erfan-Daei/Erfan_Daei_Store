using EndPoint.Site.Models;
using EndPoint.Site.Models.ViewModels.LandingPage;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Carts;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;
using System.Diagnostics;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILandingPageFacad _landingPageFacad;
        private readonly IProductFacad _productFacad;

        public HomeController(ILogger<HomeController> logger, ILandingPageFacad landingPageFacad,
            IProductFacad productFacad, ICartServices cartServices)
        {
            _logger = logger;
            _landingPageFacad = landingPageFacad;
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult Index()
        {
            LandingPageViewModel LandingViewModel = new LandingPageViewModel()
            {
                SiteImages = _landingPageFacad.GetImages_SiteService.Execute().Data,

                MostViewed1 = _productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
                {
                    CategoryId = 8,
                    Page = 1,
                }, Ordering.MostViewed, 8).Data.ProductList,

                MostViewed2 = _productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
                {
                    CategoryId = 11,
                    Page = 1,
                }, Ordering.MostViewed, 8).Data.ProductList,

                MostViewed3 = _productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
                {
                    CategoryId = 14,
                    Page = 1,
                }, Ordering.MostViewed, 8).Data.ProductList,

                Newest = _productFacad.GetProductList_SiteService.Execute(new RequestGetProductList_SiteDto
                {
                    CategoryId = 0,
                    Page = 1,
                }, Ordering.Newest, 8).Data.ProductList,
            };
            return View(LandingViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
