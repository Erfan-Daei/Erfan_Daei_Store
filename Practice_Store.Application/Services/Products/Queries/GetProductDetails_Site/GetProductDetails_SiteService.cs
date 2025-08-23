using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site
{
    public class GetProductDetails_SiteService : IGetProductDetails_Site
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IGetProductList_Site _getProductList;
        public GetProductDetails_SiteService(IDatabaseContext databaseContext, IGetProductList_Site getProductList_Site)
        {
            _databaseContext = databaseContext;
            _getProductList = getProductList_Site;
        }

        public ResultDto<GetProductDetails_SiteDto> Execute(long Id)
        {
            var _Product = _databaseContext.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.Off)
                .Include(p => p.Reviews)
                .Where(p => p.Id == Id)
                .FirstOrDefault();

            var GetSameProducts = _getProductList.Execute(new RequestGetProductList_SiteDto
            {
                Page = 1,
                SearchKey = _Product.Category.ParentCategory.Name,
            }, (Ordering)2, 4);

            var SameProducts = GetSameProducts.Data.ProductList.Where(p => p.Id != Id).ToList();

            var _Review = _Product.Reviews.ToList().Where(p => p.ReplyedReviewId == null).Select(p => new GetProductDetails_SiteReviewDto
            {
                ReviewId = p.Id,
                UserName = p.UserName,
                UserLastName = p.UserLastName,
                UserScore = (int)Math.Floor(p.Score ?? 0),
                ReviewDetail = p.ReviewDetail,
                ReviewTime = p.InsertTime,
            }).ToList();

            foreach (var review in _Review)
            {
                var Reply = _databaseContext.Reviews.FirstOrDefault(p => p.ReplyedReviewId == review.ReviewId);
                if (Reply == null)
                {
                    continue;
                }

                review.Reply = new GetProductList_SiteReplyDto
                {
                    DisplayName = "ادمین",
                    ReplyTime = Reply.InsertTime,
                    ReviewDetail = Reply.ReviewDetail,
                };
            }

            _Product.ViewCount++;
            _databaseContext.SaveChanges();

            return new ResultDto<GetProductDetails_SiteDto>()
            {
                Data = new GetProductDetails_SiteDto
                {
                    Id = Id,
                    Name = _Product.Name,
                    Brand = _Product.Brand,
                    Description = _Product.Description,
                    Price = _Product.Price,
                    CategoryName = _Product.Category.Name,
                    ParentCategoryName = _Product.Category.ParentCategory.Name,
                    OffPercentage = _Product.Off?.Percentage ?? 0,
                    ImagesSrc = _Product.ProductImages.Select(p => p.Src).ToList(),
                    Sizes = _Product.ProductSizes.ToList().Select(p => new GetProductDetails_SiteSizesDto
                    {
                        SizeId = p.Id,
                        SizeName = p.Size,
                        Inventory = p.Inventory,
                    }).ToList(),
                    SameProducts = SameProducts,
                    ProductReviewCount = _Product.ReviewCount,
                    ProductScore = (int)Math.Floor(_Product.ReviewScore),
                    ProductReviews = _Review,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
