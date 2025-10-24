using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IDeleteCategoryRepo
    {
        bool DeleteSubCategories(List<Category> subCategories);

        bool DeleteCategory(Category category);
    }
}
