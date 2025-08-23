using Practice_Store.Application.Services.Common.GetProductMenu;
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

namespace Practice_Store.Application.Interfaces.FacadPatterns
{
    public interface IProductFacad
    {
        IAddCategory AddCategoryService { get; }
        IGetCategories GetCategoriesService { get; }
        IEditCategory EditCategoryService { get; }
        IDeleteCategory DeleteCategoryService { get; }
        IAddProduct AddProductService { get; }
        IGetAllSubCategories GetAllSubCategoriesService { get; }
        IGetProductList_Admin GetProductList_AdminService { get; }
        IDeleteProduct DeleteProductService { get; }
        IChangeProductDisplayed ChangeProductDisplayedService { get; }
        IGetProductDetails_Admin GetProductDetails_AdminService { get; }
        IEditProduct EditProductService { get; }
        IGetProductList_Site GetProductList_SiteService { get; }
        IGetProductDetails_Site GetProductDetails_SiteService { get; }
        IGetProductMenu GetProductMenuService { get; }
        IAddReview AddReviewService { get; }
        IGetAllReviews GetAllReviewsService { get; }
        IAddReplyToReview AddReplyToReviewService { get; }
    }
}
