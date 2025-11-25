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

        public Review? GetReplies(Product product, long Id)
        {
            var Reply =_databaseContext.Reviews.FirstOrDefault(p => p.ReplyedReviewId == Id);
            product.ViewCount++;
            _databaseContext.SaveChanges();
            return Reply;
        }
    }
}
