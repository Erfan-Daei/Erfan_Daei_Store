using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Commands.AddReview
{
    public class AddReviewService : IAddReview
    {
        private readonly IAddReviewRepo _addReviewRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        private readonly IManageUserRepository _manageUserRepository;
        public AddReviewService(IAddReviewRepo addReviewRepo,
            IProductRepoFinders productRepoFinders,
            IManageUserRepository manageUserRepository)
        {
            _addReviewRepo = addReviewRepo;
            _productRepoFinders = productRepoFinders;
            _manageUserRepository = manageUserRepository;
        }

        public ResultDto Execute(RequestAddReview Request)
        {
            var _Product = _productRepoFinders.FindProduct(Request.ProductId);
            if (_Product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var _User = _manageUserRepository.FindUserById(Request.UserId);
            if (_User == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }
            Review Review = new Review()
            {
                Product = _Product,
                ProductId = Request.ProductId,
                User = _User,
                UserId = Request.UserId.ToString(),
                UserName = _User.Name,
                UserLastName = _User.LastName,
                Score = Request.Score,
                ReviewDetail = Request.ReviewDetail,
            };

            if (_Product.ReviewCount == 0)
            {
                _Product.ReviewScore = Request.Score;
            }
            _Product.ReviewScore = (Request.Score + _Product.ReviewScore) / 2;

            var AddReview = _addReviewRepo.AddReview(Review, _Product, _Product.ReviewScore);
            if (!AddReview)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return new ResultDto
            {
                IsSuccess = true,
                Message = "نظر شما با موفقیت ثیت شد",
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}
