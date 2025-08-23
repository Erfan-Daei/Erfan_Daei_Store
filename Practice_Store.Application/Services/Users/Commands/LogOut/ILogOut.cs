using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.LogOut
{
    public interface ILogOut
    {
        public ResultDto Execute(string UserId);
    }
}
