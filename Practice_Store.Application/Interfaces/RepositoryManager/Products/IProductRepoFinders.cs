using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products
{
    public interface IProductRepoFinders
    {
        Product? FindProduct(long ProductId);

        Category? FindCategory(long? CategoryId);
    }
}
