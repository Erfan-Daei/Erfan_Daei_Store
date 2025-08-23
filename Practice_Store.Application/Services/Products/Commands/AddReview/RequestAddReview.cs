namespace Practice_Store.Application.Services.Products.Commands.AddReview
{
    public class RequestAddReview
    {
        public long ProductId { get; set; }
        public string UserId { get; set; }

        public float Score { get; set; }
        public string? ReviewDetail { get; set; }
    }
}
