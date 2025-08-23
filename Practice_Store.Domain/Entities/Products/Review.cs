using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Domain.Entities.Products
{
    public class Review : BaseEntity
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }

        public virtual IdtUser User { get; set; }
        public string UserId { get; set; }

        public string? UserName { get; set; }
        public string? UserLastName { get; set; }

        public float? Score { get; set; }
        public string? ReviewDetail { get; set; }

        public virtual Review? ReplyedReview { get; set; }
        public long? ReplyedReviewId { get; set; }
    }
}
