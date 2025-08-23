using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUser
{
    public interface IEditUser_Site
    {
        ResultDto EditUser(RequestEditUser_SiteDto Request);
    }
}
