using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products
{
    public class ProductRepoFinders : IProductRepoFinders
    {
        private readonly IDatabaseContext _databaseContext;
        public ProductRepoFinders(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Product? FindProduct(long ProductId)
        {
            return _databaseContext.Products.Find(ProductId);
        }

        public Category? FindCategory(long? CategoryId)
        {
            return _databaseContext.Categories.Find(CategoryId);
        }
    }
}
