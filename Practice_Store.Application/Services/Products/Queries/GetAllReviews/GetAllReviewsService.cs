using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetAllReviews
{
    public class GetAllReviewsService : IGetAllReviews
    {
        private readonly IDatabaseContext _databaseContext;
        public GetAllReviewsService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<GetAllReviewsDto>> Execute(long ProductId)
        {
            var _ProductReviews = _databaseContext.Reviews.Where(p => p.ProductId == ProductId).ToList();

            var _ProductReviewsList = _ProductReviews.Where(p => p.ReplyedReviewId == null).Select(p => new GetAllReviewsDto
            {
                ReviewId = p.Id,
                ProductId = ProductId,
                UserLastName = p.UserLastName,
                UserName = p.UserName,
                UserScore = (int)Math.Floor(p.Score ?? 0),
                ReviewDetail = p.ReviewDetail,
                ReviewTime = p.InsertTime,
            }).ToList();

            foreach (var review in _ProductReviewsList)
            {
                var Reply = _databaseContext.Reviews.FirstOrDefault(p => p.ReplyedReviewId == review.ReviewId);
                if (Reply == null)
                {
                    continue;
                }

                review.Reply = new GetAllReviewsReplyDto
                {
                    DisplayName = "ادمین",
                    ReplyTime = Reply.InsertTime,
                    ReviewDetail = Reply.ReviewDetail,
                };
            }

            return new ResultDto<List<GetAllReviewsDto>>()
            {
                Data = _ProductReviewsList,
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
