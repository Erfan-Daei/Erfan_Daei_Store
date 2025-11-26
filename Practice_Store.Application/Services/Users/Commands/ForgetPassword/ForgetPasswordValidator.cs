using FluentValidation;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public class ForgetPasswordValidator : AbstractValidator<RequestForgetPasswordDto>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage("لطفا رمزعبور را وارد کنید")
                .Equal(x => x.ConPassword).WithMessage("رمزعبور و تکرار آن برابر نیست")
                .MinimumLength(8).WithMessage("رمز عبور باید حداقل 8 کاراکتر باشد")
                .MaximumLength(64).WithMessage("رمز عبور نمی تواند بیشتر از 64 کاراکتر باشد")
                .Matches("[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد")
                .Matches("[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد")
                .Matches("[0-9]").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد")
                .Matches(@"[@$!%*?&]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد")
                .WithErrorCode("400");
        }
    }
}
