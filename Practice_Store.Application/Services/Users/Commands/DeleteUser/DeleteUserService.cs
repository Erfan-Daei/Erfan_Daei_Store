using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.DeleteUser
{
    public class DeleteUserService : IDeleteUser
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IDeleteUserRepo _deleteUserRepo;
        public DeleteUserService(IUserRepoFinder userRepoFinder,
            IDeleteUserRepo deleteUserRepo)
        {
            _userRepoFinder = userRepoFinder;
            _deleteUserRepo = deleteUserRepo;
        }
        public ResultDto DeleteUser(string UserId)
        {
            var _User = _userRepoFinder.FindUserById(UserId);
            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }
            var _UserRoles = _deleteUserRepo.GetRoles(_User);
            var _DeleteRoles = _deleteUserRepo.RemoveFromRole(_User, _UserRoles);
            _User.DeletedTime = DateTime.UtcNow;
            _User.IsDeleted = true;
            _User.LockoutEnabled = true;
            var Delete = _userRepoFinder.UpdateUser(_User);
            if (!Delete.Succeeded)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
