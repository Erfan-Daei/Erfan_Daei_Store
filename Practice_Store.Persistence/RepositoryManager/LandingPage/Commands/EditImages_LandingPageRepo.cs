using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Persistence.RepositoryManager.LandingPage.Commands
{
    public class EditImages_LandingPageRepo : IEditImages_LandingPageRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public EditImages_LandingPageRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public LandingPageImages? FindImage(long Id)
        {
            return _databaseContext.LandingPageImages.Find(Id);
        }

        public List<LandingPageImages> FindAllImages()
        {
            return _databaseContext.LandingPageImages.ToList();
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
