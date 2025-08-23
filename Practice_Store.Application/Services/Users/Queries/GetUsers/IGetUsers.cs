using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsers
    {
        ResultDto<ResultGetUsersDTO> GetUsers(RequestGetUsersDto Request);
    }
}
