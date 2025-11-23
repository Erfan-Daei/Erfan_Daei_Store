using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    internal class GetProductDetail_AdminRepo : IGetProductDetail_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductDetail_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public Product GetDetail(long Id)
        {
            return _databaseContext.Products
                .Where(p => p.Id == Id)
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.Off)
                .Include(p => p.Reviews)
                .FirstOrDefault();
        }
    }
}
