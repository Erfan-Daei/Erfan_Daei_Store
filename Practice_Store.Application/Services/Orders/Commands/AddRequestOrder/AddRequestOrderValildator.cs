using FluentValidation;
using Practice_Store.Application.Services.Orders.Commands.RequestOrder;

namespace Practice_Store.Application.Services.Orders.Commands.AddRequestOrder
{
    public class AddRequestOrderValildator : AbstractValidator<RequestAddRequestOrderDto>
    {
        public AddRequestOrderValildator()
        {
            RuleFor(x => x.TotalSum)
                .GreaterThan(0).WithMessage("لطفا مجموع را وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.Shipping)
                .GreaterThan(0).WithMessage("لطفا هزینه حمل را وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("لطفا نام را وارد کنید")
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$").WithMessage("نام نمی تواند از اعداد و حروف انگلیسی و خاص تشکیل شود")
                .MaximumLength(50).WithMessage("نام نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("لطفا نام خانوادگی را وارد کنید")
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$").WithMessage("نام خانوادگی نمی تواند از اعداد و حروف انگلیسی و خاص تشکیل شود")
                .MaximumLength(50).WithMessage("نام خانوادگی نمی تواند بیشتر از 50 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.PostCode)
                .GreaterThan(0).WithMessage("کد پستی را وارد کنید")
                .InclusiveBetween(1000000000, 9999999999).WithMessage("کد پستی باید 10 رقم باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("لطفا Hnvs را وارد کنید")
                .Matches(@"^[\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC0-9,،]*$").WithMessage("آدرس نمی تواند از حروف انگلیسی و کاراکترهای خاص تشکیل شود")
                .MaximumLength(200).WithMessage("آدرس نمی تواند بیشتر از 200 کاراکتر باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Mobile)
                .NotNull().WithMessage("لطفا شماره تماس را وارد کنید")
                .Matches(@"^(0\d{10}|98\d{10})$").WithMessage("لطفت شماره موبایل را به درستی وارد کنید")
                .WithErrorCode("400");
        }
    }
}
