using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IDeleteProductRepo
    {
        bool DeleteImages(long productId);

        bool DeleteSizes(long productId);

        bool DeleteOff(long productId);

        bool DeleteProduct(Product product);
    }
}
