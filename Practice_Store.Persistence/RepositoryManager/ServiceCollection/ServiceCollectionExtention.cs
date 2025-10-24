using Microsoft.Extensions.DependencyInjection;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Persistence.RepositoryManager.Products;
using Practice_Store.Persistence.RepositoryManager.Products.Commands;

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

            return services;
        }
    }
}
