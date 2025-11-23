using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site
{
    public class GetImages_SiteService : IGetImages_Site
    {
        private readonly IGetImage_SiteRepo _getImage_SiteRepo;
        public GetImages_SiteService(IGetImage_SiteRepo getImage_SiteRepo)
        {
            _getImage_SiteRepo = getImage_SiteRepo;
        }

        public ResultDto<List<GetImages_SiteDto>> Execute()
        {
            var GetImages = _getImage_SiteRepo.GetImages()
                .Select(p => new GetImages_SiteDto
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
