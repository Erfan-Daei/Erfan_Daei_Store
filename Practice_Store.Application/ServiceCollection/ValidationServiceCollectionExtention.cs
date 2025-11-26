using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site;
using Practice_Store.Application.Services.Users.Commands.EditUser;
using Practice_Store.Application.Services.Users.Commands.EditUser_Admin;
using Practice_Store.Application.Services.Users.Commands.ForgetPassword;
using Practice_Store.Application.Services.Users.Commands.RegisterUser;
using Practice_Store.Application.Services.Users.Queries.GetUsers;
using Practice_Store.Application.Services.Users.Queries.RoleManagement;

namespace Practice_Store.Application.ServiceCollection
{
    public static class ValidationServiceCollectionExtention
    {
        public static IServiceCollection UserServicesValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();
            services.AddValidatorsFromAssemblyContaining<ChangeUserEmail_SiteValidator>();
            services.AddValidatorsFromAssemblyContaining<EditUser_SiteValidator>();
            services.AddValidatorsFromAssemblyContaining<EditUser_AdminValidator>();
            services.AddValidatorsFromAssemblyContaining<ForgetPasswordValidator>();
            services.AddValidatorsFromAssemblyContaining<GetUsersValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleManagementValidator>();
            services.AddValidatorsFromAssemblyContaining<RoleManagement_GetRolesValidator>();

            return services;
        }
    }
}
