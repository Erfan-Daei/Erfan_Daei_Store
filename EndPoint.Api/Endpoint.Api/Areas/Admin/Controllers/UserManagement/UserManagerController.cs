using Endpoint.Api.Areas.Admin.Model.Common;
using Endpoint.Api.Areas.Admin.Model.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Application.Services.Users.Queries.GetUsers;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.UserManagement
{
    [Route("api/Area/Admin/[controller]")]
    [Area("Admin")]
    [Authorize(Policy ="UserManagementAdmins")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserFacad _userFacad;
        private readonly EmailSender _emailSender;
        public UserManagerController(IUserFacad userFacad)
        {
            _userFacad = userFacad;

            _emailSender = new EmailSender(SMTPDetail._smtpHost,
                SMTPDetail._smtpPort,
                SMTPDetail._smtpUser,
                SMTPDetail._smtpPass);
        }

        [HttpGet]
        public IActionResult GET([FromQuery] PaginationDto _Request)
        {
            var Result = _userFacad.GetUsersService.GetUsers(new RequestGetUsersDto
            {
                SearchKey = _Request.SearchKey,
                Page = _Request.Page == 0 ? 1 : _Request.Page,
                PageSize = _Request.PageSize,
            });

            if (!Result.IsSuccess)
            {
                return Problem();
            }

            dynamic Output = new
            {
                UserList = Result.Data.UsersDtos,
                CurrentPage = Result.Data.CurrentPage,
                PageSize = Result.Data.PageSize,
                RowsCount = Result.Data.RowsCount,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin", UserId = "UserId" }, Request.Scheme) ?? "",
                        Rel = "UserDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = "UserId" }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewUser",
                        Method = "POST"
                    },
                }
            };
            return Ok(Output);
        }

        [HttpGet("Id")]
        public IActionResult GET(string UserId)
        {
            var Result = _userFacad.GetAdminDetailService.GetDetail(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                UserDetail = new Admin_UserDetailDto
                {
                    Id = UserId,
                    Email = Result.Data.Email,
                    EmailConfirmed = Result.Data.EmailConfirmed,
                    Name = Result.Data.Name,
                    LastName = Result.Data.LastName,
                    Address = Result.Data.Address,
                    PostCode = Result.Data.PostCode,
                    Mobile = Result.Data.Mobile,
                    MobileConfirmed = Result.Data.MobileConfirmed,
                    IsActive = Result.Data.IsActive,
                    Roles = Result.Data.Roles

                },
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = UserId }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserList",
                        Method = "GET"
                    },
                },
            };
            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] Admin_SignUpDto _Request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Result = _userFacad.RegisterUserService.ValidateUser(new RequestRegisterUserDto
            {
                Name = _Request.Name,
                LastName = _Request.LastName,
                Email = _Request.Email,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                Password = _Request.Password,
                ConPassword = _Request.ConPassword,
                Roles = _Request.Roles
            });

            if (!Result.IsSuccess)
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));

            string? CallbackUrl = Url.Action(nameof(GET), "EmailManager", new
            {
                UserId = Result.UserId,
                Token = Result.EmailValidationToken
            }, protocol: Request.Scheme);

            string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={CallbackUrl}> Link </a>";
            _emailSender.Execute(Result.UserEmail, body, "فعال سازی حساب کاربری");

            string Uri = Url.Action(nameof(GET), "UserManager", new { Area = "Admin", UserId = Result.UserId }) ?? "";

            dynamic Output = new
            {
                Message = "حساب کاربر با موفقیت ثبت شد . تاییدیه برای ایمیل کاربر ارسال گردید",
                StatusCode = StatusCodes.Status201Created,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "UserManager", new {Area = "Admin", UserId = Result.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "UserManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "UserUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { Area = "Admin", UserId = Result.UserId }, Request.Scheme) ?? "",
                        Rel = "UserDelete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager",new { Area = "Admin" },  Request.Scheme) ?? "",
                        Rel = "UserList",
                        Method = "GET"
                    },
                },
            };

            return Created(Uri, Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] Admin_EditUserDto _Request)
        {
            if (_Request.Password != _Request.ConPassword)
            {
                return BadRequest("رمز عبور و تکرار آن برابر نیست");
            }

            var Result = _userFacad.EditUser_AdminService.EditUser(new RequestEditUser_AdminDto
            {
                UserId = _Request.UserId,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                PhoneNumber = _Request.PhoneNumber,
                Email = _Request.Email,
                Password = _Request.Password,
            });

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = "اطلاعات کاربر با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "UserManager", new { _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "Self",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "UserManager", new { _Request.UserId }, Request.Scheme) ?? "",
                        Rel = "Delete",
                        Method = "DELETE"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "EmailManager", Request.Scheme) ?? "",
                        Rel = "Update_Email",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "PasswordManager", Request.Scheme) ?? "",
                        Rel = "Update_Password",
                        Method = "POST"
                    },
                },
            };

            return Ok(Output);
        }

        [HttpDelete]
        public IActionResult DELETE([FromQuery] string UserId)
        {
            var Result = _userFacad.DeleteUserService.DeleteUser(UserId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = "حساب کاربر با موفقیت حذف گردید",
                StatusCode = StatusCodes.Status200OK,
            };
            return Ok(Output);
        }
    }
}
