using FluentValidation;

namespace Practice_Store.Application.Services.Products.Commands.AddProduct
{
    public class AddProductValidator : AbstractValidator<RequestAddProductDto>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("لطفا نام محصول را وارد کنید")
                .Matches(@"^[A-Za-z0-9\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$")
                .WithMessage("نام محصول فقط باید شامل حروف فارسی، حروف انگلیسی و عدد باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Brand)
                .NotNull().WithMessage("لطفا برند محصول را وارد کنید")
                .Matches(@"^[A-Za-z0-9\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$")
                .WithMessage("برند محصول فقط باید شامل حروف فارسی، حروف انگلیسی و عدد باشد")
                .WithErrorCode("400");

            RuleFor(x => x.Description)
                .Matches(@"^[A-Za-z0-9_\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC,،]+$").WithMessage("توضیحات نمی تواند از کاراکترهای خاص تشکیل شود")
                .MaximumLength(200).WithMessage("توضیحات نمی تواند بیشتر از 200 کاراکتر باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("لطفا قیمت محصول را وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("لطفا دسته بندی محصول را وارد کنید")
                .WithErrorCode("400");

            RuleForEach(x => x.ImageSrc)
                .NotNull().WithMessage("لطفا حداقل یک عکس را برای محصول انتخاب کنید")
                .ChildRules(ch =>
                {
                    ch.RuleFor(x => x.Src)
                    .Matches(@"^[\u0600-\u06FFa-zA-Z0-9_\-.:/]+$").WithMessage("لینک تصویر را به درستی وارد کنید");
                })
                .WithErrorCode("400");

            RuleForEach(x => x.Sizes)
                .ChildRules(ch =>
                {
                    ch.RuleFor(s => s.Size)
                    .Matches(@"^[A-Za-z0-9\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$").WithMessage("سایز فقط باید شامل حروف فارسی، حروف انگلیسی و عدد باشد");

                    ch.RuleFor(s => s.Inventory)
                    .LessThan(1000).WithMessage("محصول نمیتواند از 1000 بیشتر باشد");
                })
                .NotNull().WithMessage("لطفا حداقل یک سایز را برای محصول انتخاب کنید")
                .WithErrorCode("400");

            RuleFor(x => x.OffPercentage)
                .InclusiveBetween((byte)0, (byte)100).WithMessage("درصد تخفیف باید بین 0 تا 100 باشد")
                .WithErrorCode("400")
                .When(x => x.OffPercentage.HasValue);
        }
    }
}
