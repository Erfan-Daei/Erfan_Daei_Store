using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public class ChangeUserEmail_SiteService : IChangeUserEmail_Site
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IChangeUserEmail_SiteRepo _changeUserEmail_SiteRepo;
        public ChangeUserEmail_SiteService(IUserRepoFinder userRepoFinder,
            IChangeUserEmail_SiteRepo changeUserEmail_SiteRepo)
        {
            _userRepoFinder = userRepoFinder;
            _changeUserEmail_SiteRepo = changeUserEmail_SiteRepo;
        }

        public ResultChangeUserEmail_SiteDto CheckEmailValidation(RequestChangeUserEmail_SiteDto Request)
        {
            var _User = _userRepoFinder.FindUserById(Request.UserId);
            if (_User == null)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var _GetEmail = _changeUserEmail_SiteRepo.EmailExist(Request.NewEmail);
            if (_GetEmail != null)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "این ایمیل از قبل استفاده شده است",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            _User.Email = Request.NewEmail;
            _User.UserName = Request.NewEmail;
            _User.EmailConfirmed = false;
            var Update = _userRepoFinder.UpdateUser(_User);
            if (!Update.Succeeded)
            {
                return new ResultChangeUserEmail_SiteDto
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var Token = _changeUserEmail_SiteRepo.GenerateChangeEmailToken(_User);

            return new ResultChangeUserEmail_SiteDto
            {
                IsSuccess = true,
                Message = "تاییدیه به پست الکترونیک جدید شما ارسال گردید",
                StatusCode = StatusCodes.Status202Accepted,
                EmailValidationToken = Token,
            };
        }
    }
}
