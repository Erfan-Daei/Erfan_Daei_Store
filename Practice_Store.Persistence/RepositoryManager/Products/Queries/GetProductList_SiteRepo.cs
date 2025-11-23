using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    public class GetProductList_SiteRepo : IGetProductList_SiteRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductList_SiteRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IQueryable<Product> GetProducts()
        {
            return _databaseContext.Products.Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.Off)
                .AsQueryable();
        }
    }
}
