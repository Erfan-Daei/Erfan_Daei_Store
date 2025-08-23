using Practice_Store.Common;

namespace Practice_Store.Application.Services.LandingPage.Commands.EditImages
{
    public interface IEditImage_LandingPage
    {
        ResultDto Execute(RequestEditImage_LandingPageDto Request);
    }
}
