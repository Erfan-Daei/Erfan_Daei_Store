using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries
{
    public interface IGetAllReviewsRepo
    {
        List<Review> GetReviews(long ProductId);

        Review? GetReplies(long Id);
    }
}
