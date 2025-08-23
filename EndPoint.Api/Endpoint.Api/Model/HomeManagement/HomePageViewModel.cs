using Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;

namespace Endpoint.Api.Model.HomeManagement
{
    public class HomePageViewModel
    {
        public List<GetImages_SiteDto> SiteImages { get; set; }
        public List<GetProductList_SiteDto> MostViewed1 { get; set; }
        public List<GetProductList_SiteDto> MostViewed2 { get; set; }
        public List<GetProductList_SiteDto> MostViewed3 { get; set; }
        public List<GetProductList_SiteDto> Newest { get; set; }
    }
}
