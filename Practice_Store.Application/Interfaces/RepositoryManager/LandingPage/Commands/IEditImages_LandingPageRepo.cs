using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands
{
    public interface IEditImages_LandingPageRepo
    {
        LandingPageImages? FindImage(long Id);

        List<LandingPageImages> FindAllImages();

        void Save();
    }
}
