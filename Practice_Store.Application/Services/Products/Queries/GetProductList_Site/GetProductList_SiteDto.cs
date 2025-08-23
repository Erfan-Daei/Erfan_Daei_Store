namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Site
{
    public class GetProductList_SiteDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ParentCategory { get; set; }
        public int Price { get; set; }
        public string ImageSrc { get; set; }
        public byte OffPercentage { get; set; }
        public float ProductScore { get; set; }
        public int ProductReviewCount { get; set; }
    }
}
