using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class AddCategoryRepo : IAddCategoryRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public AddCategoryRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddCategory(Category category)
        {
            try
            {
                _databaseContext.Categories.Add(category);
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
