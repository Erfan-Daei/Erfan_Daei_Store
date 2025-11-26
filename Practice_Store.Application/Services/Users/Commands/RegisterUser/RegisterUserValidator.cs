using FluentValidation;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("لطفا پست الکترونیک را وارد کنید")
                .EmailAddress().WithMessage("لطفا پست الکترونیک را به درستی وارد کنید")
                .MaximumLength(50).WithMessage("پست الکترونیک نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("لطفا رمزعبور را وارد کنید")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .MaximumLength(64).WithMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
                .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
                .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
                .Matches(@"[@$!%*?&]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد")
                .Equal(x => x.ConPassword).WithMessage("رمزعبور و تکرار آن برابر نیست")
                .WithErrorCode("400");

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

        }
    }
}
