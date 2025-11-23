using Microsoft.Extensions.DependencyInjection;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Quesries;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Queries;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Persistence.RepositoryManager.Carts.Commands;
using Practice_Store.Persistence.RepositoryManager.Carts.Queries;
using Practice_Store.Persistence.RepositoryManager.LandingPage.Commands;
using Practice_Store.Persistence.RepositoryManager.LandingPage.Queries;
using Practice_Store.Persistence.RepositoryManager.Orders.Commands;
using Practice_Store.Persistence.RepositoryManager.Orders.Queries;
using Practice_Store.Persistence.RepositoryManager.Products;
using Practice_Store.Persistence.RepositoryManager.Products.Commands;
using Practice_Store.Persistence.RepositoryManager.Products.Queries;

namespace Practice_Store.Persistence.RepositoryManager.ServiceCollection
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection RepositoriesServices(this IServiceCollection services)
        {
            services.AddScoped<IManageRepository, ManageRepository>();
            services.AddScoped<IManageUserRepository, ManageUserRepository>();

            return services;
        }

        public static IServiceCollection ProductRepositiryServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepoFinders, ProductRepoFinders>();
            services.AddScoped<IAddCategoryRepo, AddCategoryRepo>();
            services.AddScoped<IAddproductRepo, AddProductRepo>();
            services.AddScoped<IAddReplyRepo, AddReplyRepo>();
            services.AddScoped<IAddReviewRepo, AddReviewRepo>();
            services.AddScoped<IChangeProductDisplayRepo, ChangeProductDisplayRepo>();
            services.AddScoped<IDeleteCategoryRepo, DeleteCategoryRepo>();
            services.AddScoped<IDeleteProductRepo, DeleteProductRepo>();
            services.AddScoped<IEditCategoryRepo, EditCategoryRepo>();
            services.AddScoped<IEditProductRepo, EditProductRepo>();
            services.AddScoped<IGetAllReviewsRepo, GetAllReviewsRepo>();
            services.AddScoped<IGetAllSubCategoriesRepo, GetAllSubCategoriesRepo>();
            services.AddScoped<IGetCategoriesRepo, GetCategoriesRepo>();
            services.AddScoped<IGetProductDetail_AdminRepo, GetProductDetail_AdminRepo>();
            services.AddScoped<IGetProductDetails_SiteRepo, GetProductDetails_SiteRepo>();
            services.AddScoped<IGetProductList_AdminRepo, GetProductList_AdminRepo>();
            services.AddScoped<IGetProductList_SiteRepo, GetProductList_SiteRepo>();

            return services;
        }

        public static IServiceCollection CartRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAddToCartRepo, AddToCartRepo>();
            services.AddScoped<IRemoveFromCartRepo, RemoveFromCartRepo>();
            services.AddScoped<IGetCartRepo, GetCartRepo>();

            return services;
        }

        public static IServiceCollection LandingPageRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAddImage_LandingPageRepo, AddImage_LandingPageRepo>();
            services.AddScoped<IDeleteImage_LandingPageRepo, DeleteImage_LandingPageRepo>();
            services.AddScoped<IEditImages_LandingPageRepo, EditImages_LandingPageRepo>();
            services.AddScoped<IGetImage_SiteRepo, GetImage_SiteRepo>();
            services.AddScoped<IGetProductMenuRepo, GetProductMenuRepo>();

            return services;
        }

        public static IServiceCollection OrderRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IAddOrderRepo, AddOrderRepo>();
            services.AddScoped<IChangeOrderState_AdminRepo, ChangeOrderState_AdminRepo>();
            services.AddScoped<IChangeOrderState_UserRepo, ChangeOrderState_UserRepo>();
            services.AddScoped<IFailedRequestOrderRepo, FailedRequestOrderRepo>();
            services.AddScoped<IAddRequestOrderRepo, AddRequestOrderRepo>();
            services.AddScoped<IGetOrderDetails_AdminRepo, GetOrderDetails_AdminRepo>();
            services.AddScoped<IGetOrderRequest_AdminRepo, GetOrderRequest_AdminRepo>();
            services.AddScoped<IGetOrders_AdminRepo, GetOrders_AdminRepo>();
            services.AddScoped<IGetRequestOrderRepo, GetRequestOrderRepo>();
            services.AddScoped<IGetUserOrdersRepo, GetUserOrdersRepo>();

            return services;
        }
    }
}
