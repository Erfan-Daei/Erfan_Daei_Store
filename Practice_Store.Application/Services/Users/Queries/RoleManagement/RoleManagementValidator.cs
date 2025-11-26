using FluentValidation;

namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public class RoleManagementValidator : AbstractValidator<RequestRoleManagementDto>
    {
        public RoleManagementValidator()
        {
            RuleFor(x => x.RoleName)
                .Matches(@"^[A-Za-z0-9_]+$")
                .WithMessage("نام نقش فقط می‌تواند شامل حروف انگلیسی، عدد و آندرلاین (_) باشد")
                .WithErrorCode("400");

            RuleFor(x => x.NewRoleName)
                .Matches(@"^[A-Za-z0-9_]+$")
                .WithMessage("نام نقش فقط می‌تواند شامل حروف انگلیسی، عدد و آندرلاین (_) باشد")
                .WithErrorCode("400")
                .When(x => !string.IsNullOrEmpty(x.NewRoleName));
        }
    }
}
