using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands
{
    public interface IAddImage_LandingPageRepo
    {
        List<LandingPageImages> GetAllImages();

        bool AddImages(LandingPageImages image);
    }
}
