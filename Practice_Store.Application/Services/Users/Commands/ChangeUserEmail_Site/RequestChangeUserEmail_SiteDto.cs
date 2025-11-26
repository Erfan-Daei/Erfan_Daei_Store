using System.ComponentModel.DataAnnotations;

namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public class RequestChangeUserEmail_SiteDto
    {
        public string UserId { get; set; }
        public string LastEmail { get; set; }

        [EmailAddress]
        public string NewEmail { get; set; }
    }
}
