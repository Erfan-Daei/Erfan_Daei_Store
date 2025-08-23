using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Commands.AddReplyToReview
{
    public class AddReplyToReviewService : IAddReplyToReview
    {
        private readonly IDatabaseContext _databaseContext;
        public AddReplyToReviewService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<long> Execute(long ReviewId, string UserId, string ReplyDetail)
        {
            var _Review = _databaseContext.Reviews.Find(ReviewId);
            if (_Review == null)
            {
                return new ResultDto<long>
                {
                    IsSuccess = false,
                    Message = "نظر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var _User = _databaseContext.Users.Find(UserId);
            if (_User == null)
            {
                return new ResultDto<long>
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            Review Review = new Review
            {
                Product = _Review.Product,
                ProductId = _Review.ProductId,
                User = _User,
                UserId = _User.Id,
                UserName = _User.Name,
                UserLastName = _User.LastName,
                ReplyedReview = _Review,
                ReplyedReviewId = _Review.Id,
                ReviewDetail = ReplyDetail,
            };
            _databaseContext.Reviews.Add(Review);
            _databaseContext.SaveChanges();

            return new ResultDto<long> 
            {
                Data = Review.ProductId,
                IsSuccess = true,
                Message = "پاسخ شما با موفقیت ثبت شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
