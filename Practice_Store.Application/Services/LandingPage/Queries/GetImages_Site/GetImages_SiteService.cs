using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site
{
    public class GetImages_SiteService : IGetImages_Site
    {
        private readonly IDatabaseContext _databaseContext;
        public GetImages_SiteService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<GetImages_SiteDto>> Execute()
        {
            var GetImages = _databaseContext.LandingPageImages.OrderBy(p => p.ImageLocation).Select(p => new GetImages_SiteDto
            {
                Id = p.Id,
                Src = p.Src.Replace('\\', '/'),
                Title = p.Title,
                Link = p.Link,
                ImageLocation = p.ImageLocation,
            }).ToList();

            return new ResultDto<List<GetImages_SiteDto>>()
            {
                Data = GetImages,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
