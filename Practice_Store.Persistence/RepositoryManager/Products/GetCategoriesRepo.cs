namespace Practice_Store.Presistence.RepositoryManager.Products.Queries;
public class GetCategoriesRepo : IGetCategoriesRepo
{
  private readonly IDatabaseContext _databaseContext;
  public GetCategoriesRepo(IDatabaseContext databaseContext)
  {
    _databaseContext = databaseContext;
  }
  public List<CategoriesDto>> GetCategories(long? ParentId)
  {
    return _databaseContext.Categories
                .Include(p => p.ParentCategory)
                .Where(p => p.ParentCategoryId == ParentId)
                .Include(p => p.SubCategories)
                .Select(p => new CategoriesDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    HasChild = p.SubCategories.Count() > 0 ? true : false,
                    Parent = p.ParentCategory != null ?
                    new ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Name = p.ParentCategory.Name,
                    } : null,
                }).ToList();
  }
}
