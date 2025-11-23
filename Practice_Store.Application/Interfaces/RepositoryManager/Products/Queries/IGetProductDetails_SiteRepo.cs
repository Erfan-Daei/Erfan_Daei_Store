using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetProductDetails_SiteRepo
    {
        Product GetProduct(long Id);
        List<Review> GetReplies(Product product);
    }
}
