using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Admin
{
    public interface IGetProductDetails_Admin
    {
        ResultDto<GetProductDetail_AdminDto> Execute(long Id);
    }
}
