using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetProductList_SiteRepo
    {
        IQueryable<Product> GetProducts();
    }
}
