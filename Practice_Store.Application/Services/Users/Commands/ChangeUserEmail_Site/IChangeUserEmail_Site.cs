using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public interface IChangeUserEmail_Site
    {
        ResultChangeUserEmail_SiteDto CheckEmailValidation(string UserId, string LastEmail, string NewEmail);
    }
}
