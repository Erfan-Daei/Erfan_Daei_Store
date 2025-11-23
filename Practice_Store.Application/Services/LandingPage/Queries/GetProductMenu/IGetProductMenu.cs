using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetProductMenu
{
    public interface IGetProductMenu
    {
        ResultDto<List<GetProductMenuDto>> Execute();
    }
}
