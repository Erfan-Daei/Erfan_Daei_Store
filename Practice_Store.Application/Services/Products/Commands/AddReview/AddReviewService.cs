using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Commands.AddReview
{
    public class AddReviewService : IAddReview
    {
        private readonly IDatabaseContext _databaseContext;
        public AddReviewService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(RequestAddReview Request)
        {
            var _Product = _databaseContext.Products.Find(Request.ProductId);
            if (_Product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound
                };
            }

            var _User = _databaseContext.Users.Find(Request.UserId);
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
            _databaseContext.Reviews.Add(Review);

            if (_Product.ReviewCount == 0)
            {
                _Product.ReviewScore = Request.Score;
            }
            _Product.ReviewScore = (Request.Score + _Product.ReviewScore) / 2;
            _Product.ReviewCount++;
            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "نظر شما با موفقیت ثیت شد",
                StatusCode = StatusCodes.Status201Created
            };
        }
    }
}
