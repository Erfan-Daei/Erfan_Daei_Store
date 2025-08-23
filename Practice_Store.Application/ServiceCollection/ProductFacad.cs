using Microsoft.AspNetCore.Hosting;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
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

namespace Practice_Store.Application.ServiceCollection
{
    public class ProductFacad : IProductFacad
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductFacad(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        private IAddCategory _addCategoryService;
        public IAddCategory AddCategoryService
        {
            get
            {
                return _addCategoryService = _addCategoryService ?? new AddCategoryService(_databaseContext);
            }
        }

        private IGetCategories _getCategories;
        public IGetCategories GetCategoriesService
        {
            get
            {
                return _getCategories = _getCategories ?? new GetCategoriesService(_databaseContext);
            }
        }

        private IEditCategory _editCategory;
        public IEditCategory EditCategoryService
        {
            get
            {
                return _editCategory = _editCategory ?? new EditCategoryService(_databaseContext);
            }
        }

        private IDeleteCategory _deleteCategory;
        public IDeleteCategory DeleteCategoryService
        {
            get
            {
                return _deleteCategory = _deleteCategory ?? new DeleteCategoryService(_databaseContext, this);
            }
        }

        private IAddProduct _addProduct;
        public IAddProduct AddProductService
        {
            get
            {
                return _addProduct = _addProduct ?? new AddProductService(_databaseContext, _hostingEnvironment);
            }
        }

        private IGetAllSubCategories _getAllSubCategories;
        public IGetAllSubCategories GetAllSubCategoriesService
        {
            get
            {
                return _getAllSubCategories = _getAllSubCategories ?? new GetAllSubCategoriesService(_databaseContext);
            }
        }

        private IGetProductList_Admin _getProductList_Admin;
        public IGetProductList_Admin GetProductList_AdminService
        {
            get
            {
                return _getProductList_Admin = _getProductList_Admin ?? new GetProductList_AdminService(_databaseContext);
            }
        }

        private IDeleteProduct _deleteProduct;
        public IDeleteProduct DeleteProductService
        {
            get
            {
                return _deleteProduct = _deleteProduct ?? new DeleteProductService(_databaseContext);
            }
        }

        private IChangeProductDisplayed _changeProductDisplayed;
        public IChangeProductDisplayed ChangeProductDisplayedService
        {
            get
            {
                return _changeProductDisplayed = _changeProductDisplayed ?? new ChangeProductDisplayedService(_databaseContext);
            }
        }

        private IGetProductDetails_Admin _getProductDetails_Admin;
        public IGetProductDetails_Admin GetProductDetails_AdminService
        {
            get
            {
                return _getProductDetails_Admin = _getProductDetails_Admin ?? new GetProductDetails_AdminService(_databaseContext);
            }
        }

        private IEditProduct _editProduct;
        public IEditProduct EditProductService
        {
            get
            {
                return _editProduct = _editProduct ?? new EditProductService(_databaseContext, _hostingEnvironment);
            }
        }

        private IGetProductList_Site _getProductList_Site;
        public IGetProductList_Site GetProductList_SiteService
        {
            get
            {
                return _getProductList_Site = _getProductList_Site ?? new GetProductList_SiteService(_databaseContext);
            }
        }

        private IGetProductDetails_Site _getProductDetails_Site;
        public IGetProductDetails_Site GetProductDetails_SiteService
        {
            get
            {
                return _getProductDetails_Site = _getProductDetails_Site ?? new GetProductDetails_SiteService(_databaseContext, GetProductList_SiteService);
            }
        }

        private IGetProductMenu _getProductMenu;
        public IGetProductMenu GetProductMenuService
        {
            get
            {
                return _getProductMenu = _getProductMenu ?? new GetProductMenuService(_databaseContext);
            }
        }

        private IAddReview _addReview;
        public IAddReview AddReviewService
        {
            get
            {
                return _addReview = _addReview ?? new AddReviewService(_databaseContext);
            }
        }
        
        private IGetAllReviews _getAllReviews;
        public IGetAllReviews GetAllReviewsService
        {
            get
            {
                return _getAllReviews = _getAllReviews ?? new GetAllReviewsService(_databaseContext);
            }
        }
        
        private IAddReplyToReview _addReplyToReview;
        public IAddReplyToReview AddReplyToReviewService
        {
            get
            {
                return _addReplyToReview = _addReplyToReview ?? new AddReplyToReviewService(_databaseContext);
            }
        }
    }
}