namespace Endpoint.Api.Model.ProductManagement
{
    public class AddReviewDto
    {
        public long ProductId { get; set; }

        public float Score { get; set; }
        public string? ReviewDetail { get; set; }
    }
}
