using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site
{
    public class GetProductDetails_SiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }

        public byte OffPercentage { get; set; }

        public List<string> ImagesSrc { get; set; }
        public List<GetProductDetails_SiteSizesDto> Sizes { get; set; }
        public List<GetProductList_SiteDto> SameProducts { get; set; }

        public List<GetProductDetails_SiteReviewDto> ProductReviews { get; set; }
        public float ProductScore { get; set; }
        public int ProductReviewCount { get; set; }
    }
}
