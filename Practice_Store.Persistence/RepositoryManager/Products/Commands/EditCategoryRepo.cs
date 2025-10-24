using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class EditCategoryRepo : IEditCategoryRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public EditCategoryRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool EditCategory(Category category, string name)
        {
            try
            {
                category.Name = name;
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
