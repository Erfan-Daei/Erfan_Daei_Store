using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    public class GetAllSubCategoriesRepo : IGetAllSubCategoriesRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetAllSubCategoriesRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Category> GetCategories()
        {
            return _databaseContext.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId != null)
                .ToList();
        }
    }
}
