using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class AddReviewRepo : IAddReviewRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public AddReviewRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddReview(Review review, Product product, float score)
        {
            try
            {
                _databaseContext.Reviews.Add(review);
                review.Score = score;
                product.ReviewCount++;
                _databaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
