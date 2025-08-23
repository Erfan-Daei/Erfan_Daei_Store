using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.DeleteUser
{
    public interface IDeleteUser
    {
        ResultDto DeleteUser(string UserId);
    }
}
