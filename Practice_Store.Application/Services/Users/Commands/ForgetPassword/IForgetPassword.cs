using Practice_Store.Common;
using static Practice_Store.Application.Services.Users.Commands.ForgetPassword.ForgetPasswordService;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public interface IForgetPassword
    {
        ResultForgetPasswordDto CheckPassword(string UserId , string NewPassword);
        ResultDto UpdatePassword(string UserId, string Token, string NewPassword);
    }

}
