namespace Practice_Store.Persistence.RepositoryManager.Products.Queries;
public class GetAllSubCategoriesRepo : IGetAllSubCategoriesRepo
{
  private readonly IDatabaseContext _databaseContext;
  public GetAllSubCategoriesRepo(IDatabaseContext databaseContext)
  {
    _databaseContext = databaseContext;
  }
  public List<Category>? GetAllCategories()
  {
    return _databaseContext.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId != null)
                .ToList();
  }
}
