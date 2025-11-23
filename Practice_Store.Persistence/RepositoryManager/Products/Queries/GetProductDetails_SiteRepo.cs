using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site;
using Practice_Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    public class GetProductDetails_SiteRepo : IGetProductDetails_SiteRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductDetails_SiteRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Product GetProduct(long Id)
        {
            return _databaseContext.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.Off)
                .Include(p => p.Reviews)
                .Where(p => p.Id == Id)
                .FirstOrDefault();
        }

        public List<Review> GetReplies(Product product)
        {
            List<Review> result = new List<Review>();
            foreach (var review in product.Reviews)
            {
                var Reply = _databaseContext.Reviews.FirstOrDefault(p => p.ReplyedReviewId == review.Id);
                if (Reply == null)
                {
                    continue;
                }

                result.Add(review);
            }
            product.ViewCount++;
            _databaseContext.SaveChanges();
            return result;
        }
    }
}
