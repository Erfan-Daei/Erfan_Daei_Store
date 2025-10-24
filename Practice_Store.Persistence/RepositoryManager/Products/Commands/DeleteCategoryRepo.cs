using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Application.Services.Products.Queries.GetCategories;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class DeleteCategoryRepo : IDeleteCategoryRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public DeleteCategoryRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool DeleteSubCategories(List<Category> subCategories)
        {
            try
            {
                _databaseContext.Categories.RemoveRange(subCategories);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                _databaseContext.Categories.Remove(category);
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
