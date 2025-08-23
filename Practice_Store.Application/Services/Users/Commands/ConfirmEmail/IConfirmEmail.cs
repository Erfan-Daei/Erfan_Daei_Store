using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ConfirmEmail
{
    public interface IConfirmEmail
    {
        ResultDto<string> GenerateToken(string UserId);
        ResultDto ConfirmEmail(string UserId, string Token);
    }
}
