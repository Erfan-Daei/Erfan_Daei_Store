using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Queries;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.RepositoryManager.Products.Queries
{
    public class GetProductList_AdminRepo : IGetProductList_AdminRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductList_AdminRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public List<Product> GetProductList(string SearchKey, int? Page, int? PageSize)
        {
            return _databaseContext.Products
                .Include(p => p.Category)
                .Where(p => string.IsNullOrEmpty(SearchKey) ||
                            p.Name.Contains(SearchKey) ||
                            p.Brand.Contains(SearchKey) ||
                            p.Displayed.ToString().Contains(SearchKey == "نمایش" ? "true" :
                                                            SearchKey == "عدم نمایش" ? "false" : SearchKey) ||
                            p.Category.Name.Contains(SearchKey)
                )
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Include(p => p.Off)
                .OrderBy(p => p.Category.ParentCategoryId)
                .ThenByDescending(p => p.Id)
                .ToPaged(Page ?? 1, PageSize ?? 20)
                .ToList();
        }
    }
}
