using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.DeleteImage;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site;

namespace Practice_Store.Application.Interfaces.FacadPatterns
{
    public interface ILandingPageFacad
    {
        IAddImage_LandingPage AddImage_LandingPageService { get; }
        IGetImages_Site GetImages_SiteService { get; }
        IEditImage_LandingPage EditImageService { get; }
        IDeleteImage_LandingPage DeleteImage_LandingPageService { get; }
    }
}
