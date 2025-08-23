using Practice_Store.Application.Services.Users.Commands.ActivationUser;
using Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site;
using Practice_Store.Application.Services.Users.Commands.ConfirmEmail;
using Practice_Store.Application.Services.Users.Commands.DeleteUser;
using Practice_Store.Application.Services.Users.Commands.EditUser;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
using Practice_Store.Application.Services.Users.Commands.EditUserRole;
using Practice_Store.Application.Services.Users.Commands.ForgetPassword;
using Practice_Store.Application.Services.Users.Commands.LogInUsers;
using Practice_Store.Application.Services.Users.Commands.LogOut;
using Practice_Store.Application.Services.Users.Commands.RefreshToken;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Application.Services.Users.Commands.SaveToken;
using Practice_Store.Application.Services.Users.Queries.GetAdminDetail;
using Practice_Store.Application.Services.Users.Queries.GetRoles;
using Practice_Store.Application.Services.Users.Queries.GetUserDetail_Site;
using Practice_Store.Application.Services.Users.Queries.GetUserRoles;
using Practice_Store.Application.Services.Users.Queries.GetUsers;
using Practice_Store.Application.Services.Users.Queries.RoleManagement;

namespace Practice_Store.Application.Interfaces.FacadPatterns
{
    public interface IUserFacad
    {
        IActivationUser ActivationUserService { get; }
        IDeleteUser DeleteUserService { get; }
        IEditUser_Site EditUser_SiteService { get; }
        IEditUser_Admin EditUser_AdminService { get; }
        IEditUserRole EditUserRoleService { get; }
        IForgetPassword ForgetPasswordService { get; }
        ILogInUser LogInUserService { get; }
        IRegisterUser RegisterUserService { get; }
        IGetAdminDetail GetAdminDetailService { get; }
        IGetRoles GetRolesService { get; }
        IGetUserDetail_Site GetUserDetail_SiteService { get; }
        IGetUsers GetUsersService { get; }
        IChangeUserEmail_Site ChangeUserEmail_SiteService { get; }
        IGetUserRoles GetUserRolesService { get; }
        IRoleManagement RoleManagementService { get; }
        IRefreshToken RefreshTokenService { get; }
        ILogOut LogOutService { get; }
        ISaveToken SaveTokenService { get; }
        IConfirmEmail ConfirmEmailService { get; }
    }
}
