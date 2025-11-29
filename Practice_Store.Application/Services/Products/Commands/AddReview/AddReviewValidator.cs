using FluentValidation;

namespace Practice_Store.Application.Services.Products.Commands.AddReview
{
    public class AddReviewValidator : AbstractValidator<RequestAddReview>
    {
        public AddReviewValidator()
        {
            RuleFor(x => x.Score)
                .InclusiveBetween(0f, 5f).WithMessage("امتیاز باید بین 1 تا 5 باشد")
                .WithErrorCode("400");

            RuleFor(x => x.ReviewDetail)
                .Matches(@"^[A-Za-z0-9_\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC,،]+$").WithMessage("توضیحات نمی تواند از کاراکترهای خاص تشکیل شود")
                .MaximumLength(200).WithMessage("توضیحات نمی تواند بیشتر از 200 کاراکتر باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.ReviewDetail));
        }
    }
}
