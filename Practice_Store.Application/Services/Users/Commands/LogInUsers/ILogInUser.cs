using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.LogInUsers
{
    public interface ILogInUser
    {
        ResultDto<ResultLogInUserDto> Execute(string Email, string Password);
    }
}
