using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.SaveToken
{
    public interface ISaveToken
    {
        public ResultDto<(string, string)> SaveToken(string UserId, string Email, List<string> Roles);
    }
}
