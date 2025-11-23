using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Persistence.RepositoryManager.LandingPage.Commands
{
    public class DeleteImage_LandingPageRepo : IDeleteImage_LandingPageRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public DeleteImage_LandingPageRepo(IDatabaseContext databaseContext)
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

        public bool DeleteImage(LandingPageImages image)
        {
            try
            {
                _databaseContext.LandingPageImages.Remove(image);
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
