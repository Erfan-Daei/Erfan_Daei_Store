using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Queries;
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

namespace Practice_Store.Application.ServiceCollection
{
    public class UserFacad : IUserFacad
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IActivationUserRepo _activationUserRepo;
        private readonly IChangeUserEmail_SiteRepo _changeUserEmail_SiteRepo;
        private readonly IConfirmEmailRepo _confirmEmailRepo;
        private readonly IDeleteUserRepo _deleteUserRepo;
        private readonly IEditUser_AdminRepo _editUser_AdminRepo;
        private readonly IEditUserRoleRepo _editUserRoleRepo;
        private readonly IForgetPasswordRepo _forgetPasswordRepo;
        private readonly ILogInUserRepo _logInUserRepo;
        private readonly ILogOutRepo _logOutRepo;
        private readonly IRefreshTokenRepo _refreshTokenRepo;
        private readonly ISaveTokenRepo _saveTokenRepo;
        private readonly IRegisterUserRepo _registerUserRepo;
        private readonly IGetRolesRepo _getRolesRepo;
        private readonly IGetUsersRepo _getUsersRepo;
        private readonly IRoleManagementRepo _roleManagementRepo;
        private readonly IGenerateToken _generateToken;
        public UserFacad(IUserRepoFinder userRepoFinder,
            IActivationUserRepo activationUserRepo,
            IChangeUserEmail_SiteRepo changeUserEmail_SiteRepo,
            IConfirmEmailRepo confirmEmailRepo,
            IDeleteUserRepo deleteUserRepo,
            IEditUser_AdminRepo editUser_AdminRepo,
            IEditUserRoleRepo editUserRoleRepo,
            IForgetPasswordRepo forgetPasswordRepo,
            ILogInUserRepo logInUserRepo,
            ILogOutRepo logOutRepo,
            IRefreshTokenRepo refreshTokenRepo,
            ISaveTokenRepo saveTokenRepo,
            IRegisterUserRepo registerUserRepo,
            IGetRolesRepo getRolesRepo,
            IGetUsersRepo getUsersRepo,
            IRoleManagementRepo roleManagementRepo,
            IGenerateToken generateToken)
        {
            _userRepoFinder = userRepoFinder;
            _activationUserRepo = activationUserRepo;
            _changeUserEmail_SiteRepo = changeUserEmail_SiteRepo;
            _confirmEmailRepo = confirmEmailRepo;
            _deleteUserRepo = deleteUserRepo;
            _editUser_AdminRepo = editUser_AdminRepo;
            _editUserRoleRepo = editUserRoleRepo;
            _forgetPasswordRepo = forgetPasswordRepo;
            _logInUserRepo = logInUserRepo;
            _logOutRepo = logOutRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _saveTokenRepo = saveTokenRepo;
            _registerUserRepo = registerUserRepo;
            _getRolesRepo = getRolesRepo;
            _getUsersRepo = getUsersRepo;
            _roleManagementRepo = roleManagementRepo;
            _generateToken = generateToken;
        }

        private IActivationUser _activationUser;
        public IActivationUser ActivationUserService
        {
            get
            {
                return _activationUser = _activationUser ?? new ActivationUserService(_userRepoFinder, _activationUserRepo);
            }
        }

        private IDeleteUser _deleteUser;
        public IDeleteUser DeleteUserService
        {
            get
            {
                return _deleteUser = _deleteUser ?? new DeleteUserService(_userRepoFinder, _deleteUserRepo);
            }
        }

        private IEditUser_Site _editUser_Site;
        public IEditUser_Site EditUser_SiteService
        {
            get
            {
                return _editUser_Site = _editUser_Site ?? new EditUser_SiteService(_userRepoFinder);
            }
        }

        private IEditUser_Admin _editUser_Admin;
        public IEditUser_Admin EditUser_AdminService
        {
            get
            {
                return _editUser_Admin = _editUser_Admin ?? new EditUser_AdminService(_userRepoFinder, _editUser_AdminRepo);
            }
        }

        private IEditUserRole _editUserRole;
        public IEditUserRole EditUserRoleService
        {
            get
            {
                return _editUserRole = _editUserRole ?? new EditUserRoleService(_userRepoFinder, _editUserRoleRepo);
            }
        }

        private IForgetPassword _forgetPassword;
        public IForgetPassword ForgetPasswordService
        {
            get
            {
                return _forgetPassword = _forgetPassword ?? new ForgetPasswordService(_userRepoFinder, _forgetPasswordRepo);
            }
        }

        private ILogInUser _logInUser;
        public ILogInUser LogInUserService
        {
            get
            {
                return _logInUser = _logInUser ?? new LogInUserService(_userRepoFinder, _logInUserRepo);
            }
        }

        private IRegisterUser _registerUser;
        public IRegisterUser RegisterUserService
        {
            get
            {
                return _registerUser = _registerUser ?? new RegisterUserService(_userRepoFinder, _registerUserRepo);
            }
        }

        private IGetAdminDetail _getAdminDetail;
        public IGetAdminDetail GetAdminDetailService
        {
            get
            {
                return _getAdminDetail = _getAdminDetail ?? new GetAdminDetailService(_userRepoFinder);
            }
        }

        private IGetRoles _getRoles;
        public IGetRoles GetRolesService
        {
            get
            {
                return _getRoles = _getRoles ?? new GetRolesService(_getRolesRepo);
            }
        }

        private IGetUserDetail_Site _getUserDetail_Site;
        public IGetUserDetail_Site GetUserDetail_SiteService
        {
            get
            {
                return _getUserDetail_Site = _getUserDetail_Site ?? new GetUserDetail_SiteService(_userRepoFinder);
            }
        }

        private IGetUsers _getUsers;
        public IGetUsers GetUsersService
        {
            get
            {
                return _getUsers = _getUsers ?? new GetUsersService(_userRepoFinder, _getUsersRepo);
            }
        }

        private IChangeUserEmail_Site _changeUserEmail_Site;
        public IChangeUserEmail_Site ChangeUserEmail_SiteService
        {
            get
            {
                return _changeUserEmail_Site = _changeUserEmail_Site ?? new ChangeUserEmail_SiteService(_userRepoFinder, _changeUserEmail_SiteRepo);
            }
        }

        private IGetUserRoles _getUserRoles;
        public IGetUserRoles GetUserRolesService
        {
            get
            {
                return _getUserRoles = _getUserRoles ?? new GetUserRolesService(_userRepoFinder);
            }
        }

        private IRoleManagement _roleManagement;
        public IRoleManagement RoleManagementService
        {
            get
            {
                return _roleManagement = _roleManagement ?? new RoleManagementService(_roleManagementRepo);
            }
        }

        private IRefreshToken _refreshToken;
        public IRefreshToken RefreshTokenService
        {
            get
            {
                return _refreshToken = _refreshToken ?? new RefreshTokenService(_userRepoFinder, _refreshTokenRepo, _generateToken);
            }
        }

        private ILogOut _logOut;
        public ILogOut LogOutService
        {
            get
            {
                return _logOut = _logOut ?? new LogOutService(_logOutRepo);
            }
        }

        private ISaveToken _saveToken;
        public ISaveToken SaveTokenService
        {
            get
            {
                return _saveToken = _saveToken ?? new SaveTokenService(_saveTokenRepo, _generateToken);
            }
        }

        private IConfirmEmail _confirmEmail;
        public IConfirmEmail ConfirmEmailService
        {
            get
            {
                return _confirmEmail = _confirmEmail ?? new ConfirmEmailService(_userRepoFinder, _confirmEmailRepo);
            }
        }
    }
}
