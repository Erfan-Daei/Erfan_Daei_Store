using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries
{
    public interface IGetImage_SiteRepo
    {
        List<LandingPageImages> GetImages();
    }
}
