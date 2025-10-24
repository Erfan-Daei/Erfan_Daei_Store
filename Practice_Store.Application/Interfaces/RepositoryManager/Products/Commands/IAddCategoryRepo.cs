using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IAddCategoryRepo
    {
        bool AddCategory(Category category);
    }
}
