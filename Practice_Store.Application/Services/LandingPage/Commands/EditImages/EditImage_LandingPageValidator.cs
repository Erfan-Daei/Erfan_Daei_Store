using FluentValidation;

namespace Practice_Store.Application.Services.LandingPage.Commands.EditImages
{
    public class EditImage_LandingPageValidator : AbstractValidator<RequestEditImage_LandingPageDto>
    {
        public EditImage_LandingPageValidator()
        {
            RuleFor(x => x.ImageSrc)
                .NotNull().WithMessage("لطفا یک تصویر انتحاب کنید")
                .Matches(@"^[\u0600-\u06FFa-zA-Z0-9_\-.:/]+$").WithMessage("لینک تصویر را به درستی وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.Title)
                .NotNull().WithMessage("لطفا یک عنوان انتخاب کنید")
                .Matches(@"^[\u0600-\u06FFa-zA-Z0-9\u06F0-\u06F9% ]+$").WithMessage("لطفا عنوان را به درستی وارد کنید")
                .WithErrorCode("400");

            RuleFor(x => x.Link)
                .NotNull().WithMessage("لطفا لینک را وارد کنید")
                .Matches(@"^(https?:\/\/)?([a-zA-Z0-9\u0600-\u06FF-]+\.)+[a-zA-Z]{2,}(\/[^\s]*)?$").WithMessage("لطفا لینک را به درستی وارد کنید")
                .WithErrorCode("400");
        }
    }
}
