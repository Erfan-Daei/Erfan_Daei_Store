using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetProductList_AdminRepo
    {
        List<Product> GetProductList(string SearchKey, int? Page, int? PageSize);
    }
}
