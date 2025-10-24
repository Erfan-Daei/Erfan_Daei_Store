using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductService : IDeleteProduct
    {
        private readonly IDeleteProductRepo _deleteProductRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        public DeleteProductService(IDeleteProductRepo deleteProductRepo, IProductRepoFinders productRepoFinders)
        {
            _deleteProductRepo = deleteProductRepo;
            _productRepoFinders = productRepoFinders;
        }

        public ResultDto Execute(long Id)
        {
            var _Product = _productRepoFinders.FindProduct(Id);

            if (_Product == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var DeleteImage = _deleteProductRepo.DeleteImages(Id);
            if (!DeleteImage)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var DeleteSize = _deleteProductRepo.DeleteSizes(Id);
            if (!DeleteSize)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var DeleteOff = _deleteProductRepo.DeleteOff(Id);
            if (!DeleteOff)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            var Delete = _deleteProductRepo.DeleteProduct(_Product);
            if (!Delete)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
