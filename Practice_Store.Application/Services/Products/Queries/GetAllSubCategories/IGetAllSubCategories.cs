using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetAllSubCategories
{
    public interface IGetAllSubCategories
    {
        ResultDto<List<GetAllCategoriesDto>> Execute();
    }
}
