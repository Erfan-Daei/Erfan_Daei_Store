using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Site
{
    public interface IGetProductList_Site
    {
        ResultDto<ResultGetProductList_SiteDto> Execute(RequestGetProductList_SiteDto Request, Ordering Ordering, int PageSize = 20);
    }
}
