using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetAllReviews
{
    public class GetAllReviewsService : IGetAllReviews
    {
        private readonly IGetAllReviewsRepo _getAllReviewsRepo;
        public GetAllReviewsService(IGetAllReviewsRepo getAllReviewsRepo)
        {
            _getAllReviewsRepo = getAllReviewsRepo;
        }

        public ResultDto<List<GetAllReviewsDto>> Execute(long ProductId)
        {
            var _ProductReviews = _getAllReviewsRepo.GetReviews(ProductId);

            var _ProductReviewsList = _ProductReviews.Select(p => new GetAllReviewsDto
            {
                ReviewId = p.Id,
                ProductId = ProductId,
                UserLastName = p.UserLastName,
                UserName = p.UserName,
                UserScore = (int)Math.Floor(p.Score ?? 0),
                ReviewDetail = p.ReviewDetail,
                ReviewTime = p.InsertTime,
            }).ToList();

            foreach (var reply in _ProductReviews)

            {
                var Reply = _getAllReviewsRepo.GetReplies(reply.Id);

                if (Reply == null)
                {
                    continue;
                }
                _ProductReviewsList.FirstOrDefault(p => p.ReviewId == reply.ReplyedReviewId)
                    .Reply = new GetAllReviewsReplyDto
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
