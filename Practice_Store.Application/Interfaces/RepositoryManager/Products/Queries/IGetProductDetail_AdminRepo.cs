using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetProductDetail_AdminRepo
    {
        Product GetDetail(long Id);
    }
}
