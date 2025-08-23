using Microsoft.Extensions.DependencyInjection;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Carts;
using Practice_Store.Application.Services.Common.GetProductMenu;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.DeleteImage;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site;
using Practice_Store.Application.Services.Orders.Commands.AddOrder;
using Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_Admin;
using Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_User;
using Practice_Store.Application.Services.Orders.Commands.FailedRequestOrder;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;
using Practice_Store.Application.Services.Orders.Queries.GetOrderDetails_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin;
using Practice_Store.Application.Services.Orders.Queries.GetRequestOrder;
using Practice_Store.Application.Services.Orders.Queries.GetUserOrders;
using Practice_Store.Application.Services.Products.Commands.AddCategory;
using Practice_Store.Application.Services.Products.Commands.AddProduct;
using Practice_Store.Application.Services.Products.Commands.AddReplyToReview;
using Practice_Store.Application.Services.Products.Commands.AddReview;
using Practice_Store.Application.Services.Products.Commands.DeleteCategory;
using Practice_Store.Application.Services.Products.Commands.DeleteProduct;
using Practice_Store.Application.Services.Products.Commands.EditCategory;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
using Practice_Store.Application.Services.Products.Commands.ProductChangeDisplayed;
using Practice_Store.Application.Services.Products.Queries.GetAllReviews;
using Practice_Store.Application.Services.Products.Queries.GetAllSubCategories;
using Practice_Store.Application.Services.Products.Queries.GetCategories;
using Practice_Store.Application.Services.Products.Queries.GetProductDetails_Admin;
using Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Admin;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;
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
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection UserManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IUserFacad, UserFacad>();

            services.AddScoped<IGetUsers, GetUsersService>();
            services.AddScoped<IGetRoles, GetRolesService>();
            services.AddScoped<IRegisterUser, RegisterUserService>();
            services.AddScoped<IDeleteUser, DeleteUserService>();
            services.AddScoped<IActivationUser, ActivationUserService>();
            services.AddScoped<IEditUser_Site, EditUser_SiteService>();
            services.AddScoped<IEditUser_Admin, EditUser_AdminService>();
            services.AddScoped<ILogInUser, LogInUserService>();
            services.AddScoped<IForgetPassword, ForgetPasswordService>();
            services.AddScoped<IGetUserDetail_Site, GetUserDetail_SiteService>();
            services.AddScoped<IGetAdminDetail, GetAdminDetailService>();
            services.AddScoped<IEditUserRole, EditUserRoleService>();
            services.AddScoped<IChangeUserEmail_Site, ChangeUserEmail_SiteService>();
            services.AddScoped<IGetUserRoles, GetUserRolesService>();
            services.AddScoped<IRoleManagement, RoleManagementService>();
            services.AddScoped<IRefreshToken, RefreshTokenService>();   
            services.AddScoped<ILogOut, LogOutService>();   
            services.AddScoped<ISaveToken, SaveTokenService>();   
            services.AddScoped<IConfirmEmail, ConfirmEmailService>();   

            return services;
        }

        public static IServiceCollection ProductManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IProductFacad, ProductFacad>();

            services.AddScoped<IAddCategory, AddCategoryService>();
            services.AddScoped<IGetCategories, GetCategoriesService>();
            services.AddScoped<IEditCategory, EditCategoryService>();
            services.AddScoped<IDeleteCategory, DeleteCategoryService>();
            services.AddScoped<IAddProduct, AddProductService>();
            services.AddScoped<IGetAllSubCategories, GetAllSubCategoriesService>();
            services.AddScoped<IGetProductList_Admin, GetProductList_AdminService>();
            services.AddScoped<IDeleteProduct, DeleteProductService>();
            services.AddScoped<IChangeProductDisplayed, ChangeProductDisplayedService>();
            services.AddScoped<IGetProductDetails_Admin, GetProductDetails_AdminService>();
            services.AddScoped<IEditProduct, EditProductService>();
            services.AddScoped<IGetProductList_Site, GetProductList_SiteService>();
            services.AddScoped<IGetProductDetails_Site, GetProductDetails_SiteService>();
            services.AddScoped<IGetProductMenu, GetProductMenuService>();
            services.AddScoped<IAddReview, AddReviewService>();
            services.AddScoped<IGetAllReviews, GetAllReviewsService>();
            services.AddScoped<IAddReplyToReview, AddReplyToReviewService>();

            return services;
        }

        public static IServiceCollection LandingPageManagementServices(this IServiceCollection services)
        {
            services.AddScoped<ILandingPageFacad, LandingPageFacad>();

            services.AddScoped<IAddImage_LandingPage, AddImage_LandingPageService>();
            services.AddScoped<IGetImages_Site, GetImages_SiteService>();
            services.AddScoped<IEditImage_LandingPage, EditImage_LandingPageService>();
            services.AddScoped<IDeleteImage_LandingPage, DeleteImage_LandingPageService>();


            return services;
        }

        public static IServiceCollection CartManagementServices(this IServiceCollection services)
        {
            services.AddScoped<ICartServices, CartServices>();

            return services;
        }

        public static IServiceCollection OrderManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderFacad, OrderFacad>();

            services.AddScoped<IAddRequestOreder, AddRequestOrderService>();
            services.AddScoped<IGetRequestOrder, GetRequestOrderService>();
            services.AddScoped<IAddOrder, AddOrderService>();
            services.AddScoped<IGetUserOrders, GetUserOrdersService>();
            services.AddScoped<IChangeOrderState_User, ChangeOrderState_UserService>();
            services.AddScoped<IUpdateFailedRequestOrder, UpdateFailedRequestOrderService>();
            services.AddScoped<IGetOrders_Admin, GetOrders_AdminService>();
            services.AddScoped<IGetOrderDetails_Admin, GetOrderDetails_AdminService>();
            services.AddScoped<IChangeOrderState_Admin, ChangeOrderState_AdminService>();
            services.AddScoped<IGetOrderRequest_Admin, GetOrderRequest_AdminService>();

            return services;
        }
    }
}
