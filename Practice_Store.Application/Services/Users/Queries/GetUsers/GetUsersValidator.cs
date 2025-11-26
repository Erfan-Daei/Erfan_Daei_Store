using FluentValidation;

namespace Practice_Store.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersValidator : AbstractValidator<RequestGetUsersDto>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.SearchKey)
                .Matches(@"^[A-Za-z0-9\u0621-\u064A\u067E\u0686\u0698\u06A9\u06AF\u06CC]+$")
                .WithMessage("متن فقط باید شامل حروف فارسی، حروف انگلیسی و عدد باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.SearchKey));

        }
    }
}
