namespace Practice_Store.Application.Services.Products.Queries.GetAllReviews
{
    public class GetAllReviewsDto
    {
        public long ReviewId { get; set; }
        public long ProductId { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public float UserScore { get; set; }
        public string? ReviewDetail { get; set; }
        public DateTime ReviewTime { get; set; }

        public GetAllReviewsReplyDto? Reply { get; set; }
    }
}
