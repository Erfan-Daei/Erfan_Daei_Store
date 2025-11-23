using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    public class GetAllReviewsRepo : IGetAllReviewsRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetAllReviewsRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Review> GetReviews(long ProductId)
        {
            return _databaseContext.Reviews.Where(p => p.ProductId == ProductId && p.ReplyedReviewId == null).ToList();
        }

        public List<Review> GetReplies(List<Review> Reviews)
        {
            List<Review> result = new List<Review>();
            foreach (var review in Reviews)
            {
                var Reply = _databaseContext.Reviews.FirstOrDefault(p => p.ReplyedReviewId == review.Id);
                if (Reply == null)
                {
                    continue;
                }

                result.Add(review);
            }
            return result;
        }
    }
}
