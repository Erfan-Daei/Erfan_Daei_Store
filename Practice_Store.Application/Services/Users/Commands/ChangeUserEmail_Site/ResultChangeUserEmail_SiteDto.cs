using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public class ResultChangeUserEmail_SiteDto : ResultDto
    {
        public string EmailValidationToken { get; set; }
    }
}
