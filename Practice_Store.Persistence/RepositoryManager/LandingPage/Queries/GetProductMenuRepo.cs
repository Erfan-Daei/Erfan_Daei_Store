using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.LandingPage.Queries
{
    public class GetProductMenuRepo : IGetProductMenuRepo
    {
        IDatabaseContext _databaseContext;
        public GetProductMenuRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Category> GetCategories()
        {
            return _databaseContext.Categories.Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList();
        }
    }
}
