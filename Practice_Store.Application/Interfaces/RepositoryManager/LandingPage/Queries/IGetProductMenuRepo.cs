using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries
{
    public interface IGetProductMenuRepo
    {
        List<Category> GetCategories();
    }
}
