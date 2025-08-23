using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site
{
    public class GetImages_SiteDto
    {
        public long Id { get; set; }
        public string? Src {  get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public LandingPageImageLocation ImageLocation { get; set; }

    }
}
