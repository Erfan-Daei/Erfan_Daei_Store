using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Persistence.RepositoryManager.LandingPage.Commands
{
    public class AddImage_LandingPageRepo : IAddImage_LandingPageRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public AddImage_LandingPageRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<LandingPageImages> GetAllImages()
        {
            return _databaseContext.LandingPageImages.ToList();
        }

        public bool AddImages(LandingPageImages image)
        {
            try
            {
                _databaseContext.LandingPageImages.Add(image);
                _databaseContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
