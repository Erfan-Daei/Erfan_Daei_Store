using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Application.Services.Orders.Commands.AddRequestOrder;
using Practice_Store.Application.Services.Products.Commands.AddCategory;
using Practice_Store.Application.Services.Products.Commands.AddProduct;
using Practice_Store.Application.Services.Products.Commands.AddReplyToReview;
using Practice_Store.Application.Services.Products.Commands.AddReview;
using Practice_Store.Application.Services.Products.Commands.EditCategory;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
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

        public static IServiceCollection ProductServicesValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddProductValidator>();
            services.AddValidatorsFromAssemblyContaining<AddReviewValidator>();
            services.AddValidatorsFromAssemblyContaining<EditProductValidator>();
            services.AddValidatorsFromAssemblyContaining<AddCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<EditCategoryValidator>();
            services.AddValidatorsFromAssemblyContaining<AddReplyToReviewValidator>();
            return services;
        }

        public static IServiceCollection LandingPageServicesValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddImage_LandingPageValidator>();
            services.AddValidatorsFromAssemblyContaining<EditImage_LandingPageValidator>();

            return services;
        }

        public static IServiceCollection OrderServicesValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddRequestOrderValildator>();

            return services;
        }
    }
}
