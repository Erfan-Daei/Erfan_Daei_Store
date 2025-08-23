namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site
{
    public class GetProductDetails_SiteReviewDto
    {
        public long ReviewId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public float UserScore { get; set; }
        public string? ReviewDetail { get; set; }
        public DateTime ReviewTime { get; set; }

        public GetProductList_SiteReplyDto? Reply { get; set; }
    }
}
