using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetCategories
{
    public interface IGetCategories
    {
        ResultDto<List<CategoriesDto>> Execute(long? ParentId);
    }
}
