namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
public interface IGetCategoriesRepo
{
    List<CategoriesDto>> GetCategories(long? ParentId);
}
