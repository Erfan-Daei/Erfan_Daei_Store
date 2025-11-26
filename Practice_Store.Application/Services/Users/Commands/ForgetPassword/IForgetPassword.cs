using Practice_Store.Common;
using static Practice_Store.Application.Services.Users.Commands.ForgetPassword.ForgetPasswordService;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public interface IForgetPassword
    {
        ResultForgetPasswordDto CheckPassword(RequestForgetPasswordDto Request);
        ResultDto UpdatePassword(string UserId, string Token, string NewPassword);
    }

}
