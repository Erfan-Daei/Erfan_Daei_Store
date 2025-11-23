using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetCategoriesRepo
    {
        List<Category> GetCategories(long? ParentId);
    }
}
