using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Commands.AddImages
{
    public interface IAddImage_LandingPage
    {
        ResultDto Execute(RequestAddImage_LandingPageDto Request);
    }
}
