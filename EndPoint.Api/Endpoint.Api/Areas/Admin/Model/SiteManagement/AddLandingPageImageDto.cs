using Practice_Store.Domain.Entities.LandingPage;

namespace Endpoint.Api.Areas.Admin.Model.SiteManagement
{
    public class AddLandingPageImageDto
    {
        public string ImageLink { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public LandingPageImageLocation Location { get; set; }
    }
}
