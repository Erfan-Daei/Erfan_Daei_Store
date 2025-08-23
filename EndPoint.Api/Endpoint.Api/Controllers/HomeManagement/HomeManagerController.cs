using Endpoint.Api.Model.HomeManagement;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;
using System.Diagnostics;

namespace Endpoint.Api.Controllers.HomeManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeManagerController : ControllerBase
    {
        private readonly ILogger<HomeManagerController> _logger;
        private readonly ILandingPageFacad _landingPageFacad;
        private readonly IProductFacad _productFacad;
        public HomeManagerController(ILogger<HomeManagerController> logger,
            ILandingPageFacad landingPageFacad,
            IProductFacad productFacad)
        {
            _logger = logger;
            _landingPageFacad = landingPageFacad;
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult GET()
        {
            HomePageViewModel HomePageDto = new HomePageViewModel()
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

            return Ok(HomePageDto);
        }

        [HttpGet]
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ERROR()
        {
            return StatusCode(404, new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
