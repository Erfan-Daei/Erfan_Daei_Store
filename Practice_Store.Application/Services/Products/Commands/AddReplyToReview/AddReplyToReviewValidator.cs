using FluentValidation;

namespace Practice_Store.Application.Services.Products.Commands.AddReplyToReview
{
    public class AddReplyToReviewValidator : AbstractValidator<RequestAddReplyToReviewDto>
    {
        public AddReplyToReviewValidator()
        {
            RuleFor(x => x.ReviewId)
                .GreaterThan(0).WithMessage("لطفا Id نظر را وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.ReplyDetail)
                .NotNull().WithMessage("لطفا توضیحات را وارد کنید")
                .Matches(@"^[A-Za-z0-9_\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC,،]+$").WithMessage("توضیحات نمی تواند از کاراکترهای خاص تشکیل شود")
                .MaximumLength(200).WithMessage("توضیحات نمی تواند بیشتر از 200 کاراکتر باشد")
                .WithErrorCode("400");
        }
    }
}
