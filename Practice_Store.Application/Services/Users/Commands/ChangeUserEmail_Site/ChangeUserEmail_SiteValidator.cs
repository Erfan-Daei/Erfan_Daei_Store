using FluentValidation;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public class ChangeUserEmail_SiteValidator : AbstractValidator<RequestChangeUserEmail_SiteDto>
    {
        public ChangeUserEmail_SiteValidator()
        {
            RuleFor(x => x.NewEmail)
                .NotNull().WithMessage("لطفا پست الکترونیک جدید را وارد کنید")
                .NotEqual(x => x.LastEmail).WithMessage("پست الکترونیک جدید نمی تواند با قبلی یکی باشد")
                .EmailAddress().WithMessage("لطفا پست الکترونیک را به درستی وارد کنید")
                .MaximumLength(50).WithMessage("پست الکترونیک نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400");
        }
    }
}
