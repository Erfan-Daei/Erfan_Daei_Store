using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    internal class GetCategoriesRepo : IGetCategoriesRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetCategoriesRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Category> GetCategories(long? ParentId)
        {
            return _databaseContext.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId == ParentId)
                .Include(p => p.SubCategories)
                .ToList();
        }
    }
}
