using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.JWTToken;
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
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.ServiceCollection
{
    public class UserFacad : IUserFacad
    {
        private readonly IManageUserRepository _manageUserRepository;
        private readonly IDatabaseContext _databaseContext;
        private readonly UserManager<IdtUser> _userManager;
        private readonly RoleManager<IdtRole> _roleManager;
        private readonly IGenerateToken _generateToken;
        private readonly IConfiguration _configuration;
        public UserFacad(IManageUserRepository manageUserRepository,
            IDatabaseContext databaseContext,
            UserManager<IdtUser> userManager,
            RoleManager<IdtRole> roleManager,
            IGenerateToken generateToken,
            IConfiguration configuration)
        {
            _manageUserRepository = manageUserRepository;
            _databaseContext = databaseContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _generateToken = generateToken;
            _configuration = configuration;
        }

        private IActivationUser _activationUser;
        public IActivationUser ActivationUserService
        {
            get
            {
                return _activationUser = _activationUser ?? new ActivationUserService(_manageUserRepository);
            }
        }

        private IDeleteUser _deleteUser;
        public IDeleteUser DeleteUserService
        {
            get
            {
                return _deleteUser = _deleteUser ?? new DeleteUserService(_userManager);
            }
        }

        private IEditUser_Site _editUser_Site;
        public IEditUser_Site EditUser_SiteService
        {
            get
            {
                return _editUser_Site = _editUser_Site ?? new EditUser_SiteService(_userManager);
            }
        }

        private IEditUser_Admin _editUser_Admin;
        public IEditUser_Admin EditUser_AdminService
        {
            get
            {
                return _editUser_Admin = _editUser_Admin ?? new EditUser_AdminService(_userManager);
            }
        }

        private IEditUserRole _editUserRole;
        public IEditUserRole EditUserRoleService
        {
            get
            {
                return _editUserRole = _editUserRole ?? new EditUserRoleService(_userManager, _roleManager);
            }
        }

        private IForgetPassword _forgetPassword;
        public IForgetPassword ForgetPasswordService
        {
            get
            {
                return _forgetPassword = _forgetPassword ?? new ForgetPasswordService(_userManager);
            }
        }

        private ILogInUser _logInUser;
        public ILogInUser LogInUserService
        {
            get
            {
                return _logInUser = _logInUser ?? new LogInUserService(_userManager, _generateToken, _databaseContext, _configuration);
            }
        }

        private IRegisterUser _registerUser;
        public IRegisterUser RegisterUserService
        {
            get
            {
                return _registerUser = _registerUser ?? new RegisterUserService(_userManager, _roleManager);
            }
        }

        private IGetAdminDetail _getAdminDetail;
        public IGetAdminDetail GetAdminDetailService
        {
            get
            {
                return _getAdminDetail = _getAdminDetail ?? new GetAdminDetailService(_userManager);
            }
        }

        private IGetRoles _getRoles;
        public IGetRoles GetRolesService
        {
            get
            {
                return _getRoles = _getRoles ?? new GetRolesService(_databaseContext);
            }
        }

        private IGetUserDetail_Site _getUserDetail_Site;
        public IGetUserDetail_Site GetUserDetail_SiteService
        {
            get
            {
                return _getUserDetail_Site = _getUserDetail_Site ?? new GetUserDetail_SiteService(_userManager);
            }
        }

        private IGetUsers _getUsers;
        public IGetUsers GetUsersService
        {
            get
            {
                return _getUsers = _getUsers ?? new GetUsersService(_userManager, _databaseContext);
            }
        }

        private IChangeUserEmail_Site _changeUserEmail_Site;
        public IChangeUserEmail_Site ChangeUserEmail_SiteService
        {
            get
            {
                return _changeUserEmail_Site = _changeUserEmail_Site ?? new ChangeUserEmail_SiteService(_manageUserRepository);
            }
        }

        private IGetUserRoles _getUserRoles;
        public IGetUserRoles GetUserRolesService
        {
            get
            {
                return _getUserRoles = _getUserRoles ?? new GetUserRolesService(_userManager);
            }
        }

        private IRoleManagement _roleManagement;
        public IRoleManagement RoleManagementService
        {
            get
            {
                return _roleManagement = _roleManagement ?? new RoleManagementService(_roleManager);
            }
        }

        private IRefreshToken _refreshToken;
        public IRefreshToken RefreshTokenService
        {
            get
            {
                return _refreshToken = _refreshToken ?? new RefreshTokenService(_databaseContext, _configuration, _userManager, _generateToken);
            }
        }

        private ILogOut _logOut;
        public ILogOut LogOutService
        {
            get
            {
                return _logOut = _logOut ?? new LogOutService(_databaseContext);
            }
        }

        private ISaveToken _saveToken;
        public ISaveToken SaveTokenService
        {
            get
            {
                return _saveToken = _saveToken ?? new SaveTokenService(_generateToken, _databaseContext, _configuration);
            }
        }

        private IConfirmEmail _confirmEmail;
        public IConfirmEmail ConfirmEmailService
        {
            get
            {
                return _confirmEmail = _confirmEmail ?? new ConfirmEmailService(_userManager);
            }
        }
    }
}
