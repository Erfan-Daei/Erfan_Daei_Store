using Practice_Store.Domain.Entities.LandingPage;

namespace Endpoint.Api.Areas.Admin.Model.SiteManagement
{
    public class EditLandingPageImageDto
    {
        public long Id { get; set; }
        public string ImageLink { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public LandingPageImageLocation ImageLocation { get; set; }
    }
}
