using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site
{
    public interface IGetImages_Site
    {
        ResultDto<List<GetImages_SiteDto>> Execute();
    }
}
