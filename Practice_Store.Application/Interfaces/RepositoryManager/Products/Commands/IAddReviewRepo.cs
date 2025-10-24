using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IAddReviewRepo
    {
        bool AddReview(Review review, Product product, float score);
    }
}
