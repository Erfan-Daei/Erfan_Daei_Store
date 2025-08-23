using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUser_Admin
{
    public interface IEditUser_Admin
    {
        ResultDto EditUser(RequestEditUser_AdminDto Request);
    }
}
