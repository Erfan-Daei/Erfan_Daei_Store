using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Persistence.RepositoryManager.LandingPage.Queries
{
    public class GetImage_SiteRepo : IGetImage_SiteRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetImage_SiteRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<LandingPageImages> GetImages()
        {
            return _databaseContext.LandingPageImages
                .OrderBy(p => p.ImageLocation)
                .ToList();
        }
    }
}
