using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetAllReviews
{
    public interface IGetAllReviews
    {
        ResultDto<List<GetAllReviewsDto>> Execute(long ProductId);
    }
}
