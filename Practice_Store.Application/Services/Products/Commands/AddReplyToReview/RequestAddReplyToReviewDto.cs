namespace Practice_Store.Application.Services.Products.Commands.AddReplyToReview
{
    public class RequestAddReplyToReviewDto
    {
        public long ReviewId { get; set; }
        public string UserId { get; set; }
        public string ReplyDetail { get; set; }
    }
}
