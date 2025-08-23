using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public interface IRegisterUser
    {
        ResultRegisterUserDto ValidateUser(RequestRegisterUserDto Request);
        ResultRegisterUserDto ValidateEmail(string UserId, string EmailValidationToken);
    }
}
