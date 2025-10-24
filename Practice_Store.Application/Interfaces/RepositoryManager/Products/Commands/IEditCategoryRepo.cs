using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IEditCategoryRepo
    {
        bool EditCategory(Category category, string name);
    }
}
