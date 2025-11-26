using FluentValidation;

namespace Practice_Store.Application.Services.Users.Commands.EditUser
{
    public class EditUser_SiteValidator : AbstractValidator<RequestEditUser_SiteDto>
    {
        public EditUser_SiteValidator()
        {
            RuleFor(x => x.Name)
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$").WithMessage("نام نمی تواند از اعداد و حروف انگلیسی و خاص تشکیل شود")
                .MaximumLength(50).WithMessage("نام نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.LastName)
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$").WithMessage("نام خانوادگی نمی تواند از اعداد و حروف انگلیسی و خاص تشکیل شود")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.PostCode)
                .InclusiveBetween(1000000000, 9999999999).WithMessage("کد پستی باید 10 رقم باشد")
                .WithErrorCode("400")
                .When(x => x.PostCode.HasValue);

            RuleFor(x => x.Address)
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC0-9,،]*$").WithMessage("آدرس نمی تواند از حروف انگلیسی و کاراکترهای خاص تشکیل شود")
                .MaximumLength(200).WithMessage("آدرس نمی تواند بیشتر از 200 کاراکتر باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.Address));

            RuleFor(x => x.Mobile)
                .Matches(@"^(0\d{10}|98\d{10})$").WithMessage("لطفت شماره موبایل را به درستی وارد کنید")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.Mobile));
        }
    }
}
