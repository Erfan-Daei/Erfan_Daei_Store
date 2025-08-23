using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
using Practice_Store.Application.Services.Users.Commands.EditUserRole;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Application.Services.Users.Queries.GetUsers;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,UserManagement_Admin")]
    public class UserController : Controller
    {
        private readonly IUserFacad _userFacad;
        public UserController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            ViewBag.Roles = new SelectList(_userFacad.GetRolesService.Execute().Data, "Id", "Name");
            return View("CreateRegisterUser");
        }

        [HttpPost]
        public IActionResult CreateRegisterUser(RequestRegisterUserDto _Request)
        {
            return Json(_userFacad.RegisterUserService.ValidateUser(new RequestRegisterUserDto
            {
                Name = _Request.Name,
                LastName = _Request.LastName,
                Email = _Request.Email,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                Password = _Request.Password,
                ConPassword = _Request.ConPassword,
                Roles = _Request.Roles
            }));
        }

        [HttpGet]
        public IActionResult UserList(RequestGetUsersDto _Request)
        {
            ViewBag.Roles = new SelectList(_userFacad.GetRolesService.Execute().Data, "Id", "Name");
            return View(_userFacad.GetUsersService.GetUsers(new RequestGetUsersDto
            {
                SearchKey = _Request.SearchKey,
                Page = _Request.Page == 0 ? 1 : _Request.Page,
                PageSize = _Request.PageSize,
            }).Data);
        }

        [HttpPatch]
        public IActionResult DeleteUser(string UserId)
        {
            return Json(_userFacad.DeleteUserService.DeleteUser(UserId));
        }

        [HttpPatch]
        public IActionResult ActivationUser(string UserId)
        {
            return Json(_userFacad.ActivationUserService.ChangeActivationState(UserId));
        }

        [HttpPut]
        public IActionResult EditUser(RequestEditUser_AdminDto _Request)
        {
            return Json(_userFacad.EditUser_AdminService.EditUser(new RequestEditUser_AdminDto
            {
                UserId = _Request.UserId,
                Name = _Request.Name,
                LastName = _Request.LastName,
                Address = _Request.Address,
                PostCode = _Request.PostCode,
                PhoneNumber = _Request.PhoneNumber,
                Email = _Request.Email,
                Password = _Request.Password,
            }));
        }

        [HttpPatch]
        [Authorize("Admin")]
        public IActionResult EditUserRole(RequestEditUserRole _Request)
        {
            return Json(_userFacad.EditUserRoleService.AddRoles(new RequestEditUserRole
            {
                UserId = _Request.UserId,
                Roles = _Request.Roles
            }));
        }
    }
}
