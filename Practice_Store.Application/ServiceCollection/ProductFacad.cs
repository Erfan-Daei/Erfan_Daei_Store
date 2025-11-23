using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
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
        private readonly IManageUserRepository _manageUserRepository;
        private readonly IProductRepoFinders _productRepoFinders;
        private readonly IAddCategoryRepo _addCategoryRepo;
        private readonly IAddproductRepo _addproductRepo;
        private readonly IAddReplyRepo _addreplyRepo;
        private readonly IAddReviewRepo _addreviewRepo;
        private readonly IDeleteCategoryRepo _deleteCategoryRepo;
        private readonly IDeleteProductRepo _deleteproductRepo;
        private readonly IEditCategoryRepo _editCategoryRepo;
        private readonly IChangeProductDisplayRepo _changeProductDisplayRepo;
        private readonly IEditProductRepo _editProductRepo;
        private readonly IGetAllReviewsRepo _getAllReviewsRepo;
        private readonly IGetAllSubCategoriesRepo _getAllSubCategoriesRepo;
        private readonly IGetCategoriesRepo _getCategoriesRepo;
        private readonly IGetProductDetail_AdminRepo _getProductDetail_AdminRepo;
        private readonly IGetProductDetails_SiteRepo _getProductDetail_SiteRepo;
        private readonly IGetProductList_AdminRepo _getProductList_AdminRepo;
        private readonly IGetProductList_SiteRepo _getProductList_SiteRepo;
        public ProductFacad(
        IDatabaseContext databaseContext,
        IManageUserRepository manageUserRepository,
        IProductRepoFinders productRepoFinders,
        IAddCategoryRepo addCategoryRepo,
        IAddproductRepo addProductRepo,
        IAddReplyRepo addReplyRepo,
        IAddReviewRepo addReviewRepo,
        IDeleteCategoryRepo deleteCategoryRepo,
        IDeleteProductRepo deleteProductRepo,
        IEditCategoryRepo editCategoryRepo,
        IChangeProductDisplayRepo changeProductDisplayRepo,
        IEditProductRepo editProductRepo,
        IGetAllReviewsRepo getAllReviewsRepo,
        IGetAllSubCategoriesRepo getAllSubCategoriesRepo,
        IGetCategoriesRepo getCategoriesRepo,
        IGetProductDetail_AdminRepo getProductDetail_AdminRepo,
        IGetProductDetails_SiteRepo getProductDetail_SiteRepo,
        IGetProductList_AdminRepo getProductList_AdminRepo,
        IGetProductList_SiteRepo getProductList_SiteRepo)
        {
            _databaseContext = databaseContext;
            _manageUserRepository = manageUserRepository;
            _productRepoFinders = productRepoFinders;
            _addCategoryRepo = addCategoryRepo;
            _addproductRepo = addProductRepo;
            _addreplyRepo = addReplyRepo;
            _addreviewRepo = addReviewRepo;
            _deleteCategoryRepo = deleteCategoryRepo;
            _deleteproductRepo = deleteProductRepo;
            _editCategoryRepo = editCategoryRepo;
            _changeProductDisplayRepo = changeProductDisplayRepo;
            _editProductRepo = editProductRepo;
            _getAllReviewsRepo = getAllReviewsRepo;
            _getAllSubCategoriesRepo = getAllSubCategoriesRepo;
            _getCategoriesRepo = getCategoriesRepo;
            _getProductDetail_AdminRepo = getProductDetail_AdminRepo;
            _getProductDetail_SiteRepo = getProductDetail_SiteRepo;
            _getProductList_AdminRepo = getProductList_AdminRepo;
            _getProductList_SiteRepo = getProductList_SiteRepo;
        }

        private IAddCategory _addCategoryService;
        public IAddCategory AddCategoryService
        {
            get
            {
                return _addCategoryService = _addCategoryService ?? new AddCategoryService(_addCategoryRepo, _productRepoFinders);
            }
        }

        private IGetCategories _getCategories;
        public IGetCategories GetCategoriesService
        {
            get
            {
                return _getCategories = _getCategories ?? new GetCategoriesService(_getCategoriesRepo);
            }
        }

        private IEditCategory _editCategory;
        public IEditCategory EditCategoryService
        {
            get
            {
                return _editCategory = _editCategory ?? new EditCategoryService(_editCategoryRepo, _productRepoFinders);
            }
        }

        private IDeleteCategory _deleteCategory;
        public IDeleteCategory DeleteCategoryService
        {
            get
            {
                return _deleteCategory = _deleteCategory ?? new DeleteCategoryService(_productRepoFinders, _deleteCategoryRepo, this);
            }
        }

        private IAddProduct _addProduct;
        public IAddProduct AddProductService
        {
            get
            {
                return _addProduct = _addProduct ?? new AddProductService(_productRepoFinders, _addproductRepo);
            }
        }

        private IGetAllSubCategories _getAllSubCategories;
        public IGetAllSubCategories GetAllSubCategoriesService
        {
            get
            {
                return _getAllSubCategories = _getAllSubCategories ?? new GetAllSubCategoriesService(_getAllSubCategoriesRepo);
            }
        }

        private IGetProductList_Admin _getProductList_Admin;
        public IGetProductList_Admin GetProductList_AdminService
        {
            get
            {
                return _getProductList_Admin = _getProductList_Admin ?? new GetProductList_AdminService(_getProductList_AdminRepo);
            }
        }

        private IDeleteProduct _deleteProduct;
        public IDeleteProduct DeleteProductService
        {
            get
            {
                return _deleteProduct = _deleteProduct ?? new DeleteProductService(_deleteproductRepo, _productRepoFinders);
            }
        }

        private IChangeProductDisplayed _changeProductDisplayed;
        public IChangeProductDisplayed ChangeProductDisplayedService
        {
            get
            {
                return _changeProductDisplayed = _changeProductDisplayed ?? new ChangeProductDisplayedService(_changeProductDisplayRepo, _productRepoFinders);
            }
        }

        private IGetProductDetails_Admin _getProductDetails_Admin;
        public IGetProductDetails_Admin GetProductDetails_AdminService
        {
            get
            {
                return _getProductDetails_Admin = _getProductDetails_Admin ?? new GetProductDetails_AdminService(_getProductDetail_AdminRepo, _productRepoFinders);
            }
        }

        private IEditProduct _editProduct;
        public IEditProduct EditProductService
        {
            get
            {
                return _editProduct = _editProduct ?? new EditProductService(_editProductRepo, _productRepoFinders);
            }
        }

        private IGetProductList_Site _getProductList_Site;
        public IGetProductList_Site GetProductList_SiteService
        {
            get
            {
                return _getProductList_Site = _getProductList_Site ?? new GetProductList_SiteService(_getProductList_SiteRepo);
            }
        }

        private IGetProductDetails_Site _getProductDetails_Site;
        public IGetProductDetails_Site GetProductDetails_SiteService
        {
            get
            {
                return _getProductDetails_Site = _getProductDetails_Site ?? new GetProductDetails_SiteService(_getProductDetail_SiteRepo, GetProductList_SiteService);
            }
        }

        private IAddReview _addReview;
        public IAddReview AddReviewService
        {
            get
            {
                return _addReview = _addReview ?? new AddReviewService(_addreviewRepo, _productRepoFinders, _manageUserRepository);
            }
        }

        private IGetAllReviews _getAllReviews;
        public IGetAllReviews GetAllReviewsService
        {
            get
            {
                return _getAllReviews = _getAllReviews ?? new GetAllReviewsService(_getAllReviewsRepo);
            }
        }

        private IAddReplyToReview _addReplyToReview;
        public IAddReplyToReview AddReplyToReviewService
        {
            get
            {
                return _addReplyToReview = _addReplyToReview ?? new AddReplyToReviewService(_manageUserRepository, _addreplyRepo);
            }
        }
    }
}