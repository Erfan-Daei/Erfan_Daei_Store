using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.AddReplyToReview
{
    public interface IAddReplyToReview
    {
        ResultDto<long> Execute(RequestAddReplyToReviewDto Request);
    }
}
