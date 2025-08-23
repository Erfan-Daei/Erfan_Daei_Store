using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site
{
    public interface IGetProductDetails_Site
    {
        ResultDto<GetProductDetails_SiteDto> Execute(long Id);
    }
}
