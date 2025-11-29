using FluentValidation;

namespace Practice_Store.Application.Services.Products.Commands.AddCategory
{
    public class AddCategoryValidator : AbstractValidator<RequestAddCategoryDto>
    {
        public AddCategoryValidator()
        {
            RuleFor(x => x.ParentId)
                .GreaterThan(0).WithMessage("لطفا Id دسته بندی والد را به درستی وارد کنید")
                .WithErrorCode("400")
                .When(x => x.ParentId.HasValue);

            RuleFor(x => x.Name)
                .NotNull().WithMessage("لطفا نام دسته بندی را وارد کنید")
                .Matches(@"^[\u0621-\u0628\u062A-\u063A\u0641-\u0642\u0644-\u0648\u064A\u067E\u0686\u0698\u06A9\u06AF\u06BE\u06CC\s]+$").WithMessage("نام دسته بندی نمی تواند شامل حروف انگلیسی و اعداد و کاراکتر ها خاص باشد")
                .WithErrorCode("400");
        }
    }
}
