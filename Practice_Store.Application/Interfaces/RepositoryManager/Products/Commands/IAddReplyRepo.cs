using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IAddReplyRepo
    {
        Review? FindReview(long reviewId);

        bool AddReply(Review review);
    }
}
