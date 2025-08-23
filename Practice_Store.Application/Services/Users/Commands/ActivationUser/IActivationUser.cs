using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ActivationUser
{
    public interface IActivationUser
    {
        ResultDto<ResultActivationUserDto> ChangeActivationState(string UserId);
    }
}
