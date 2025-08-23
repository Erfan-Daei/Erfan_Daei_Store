using Practice_Store.Domain.Entities.Commons;

namespace Practice_Store.Domain.Entities.LandingPage
{
    public class LandingPageImages : BaseEntity
    {
        public string Src { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public LandingPageImageLocation ImageLocation { get; set; }

    }
}
