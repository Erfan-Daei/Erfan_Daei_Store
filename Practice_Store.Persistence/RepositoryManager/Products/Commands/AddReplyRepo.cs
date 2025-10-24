using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class AddReplyRepo : IAddReplyRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public AddReplyRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Review? FindReview(long reviewId)
        {
            return _databaseContext.Reviews.Find(reviewId);
        }

        public bool AddReply(Review review)
        {
            try
            {
                _databaseContext.Reviews.Add(review);
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
