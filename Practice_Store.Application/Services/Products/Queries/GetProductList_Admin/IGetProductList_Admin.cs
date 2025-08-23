using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public interface IGetProductList_Admin
    {
        ResultDto<ResultGetProductList_AdminDto> Execute(RequestGetProductList_AdminDto Request);
    }
}
