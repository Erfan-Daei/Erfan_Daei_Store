using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.EditUser
{
    public class EditUser_SiteService : IEditUser_Site
    {
        private readonly IUserRepoFinder _userRepoFinder;
        public EditUser_SiteService(IUserRepoFinder userRepoFinder)
        {
            _userRepoFinder = userRepoFinder;
        }
        public ResultDto EditUser(RequestEditUser_SiteDto Request)
        {
            var _User = _userRepoFinder.FindUserById(Request.UserId);

            if (_User == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            if (!string.IsNullOrEmpty(Request.Name))
            {
                _User.Name = Request.Name;
            }

            if (!string.IsNullOrEmpty(Request.LastName))
            {
                _User.LastName = Request.LastName;
            }

            if (string.IsNullOrEmpty(Request.Address))
            {
                Request.Address = "-";
            }

            if (!string.IsNullOrEmpty(Request.Address))
            {
                _User.Address = Request.Address;
            }

            if (Request.PostCode.HasValue)
            {
                _User.PostCode = Request.PostCode;
            }

            if (!string.IsNullOrEmpty(Request.Mobile))
            {
                _User.PhoneNumber = Request.Mobile;
                _User.PhoneNumberConfirmed = false;
            }

            _User.UpdateTime = DateTime.UtcNow;

            var Update = _userRepoFinder.UpdateUser(_User);
            if (!Update.Succeeded)
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
                Message = "اطلاعات بروز شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
