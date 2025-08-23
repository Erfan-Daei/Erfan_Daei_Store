using Microsoft.AspNetCore.Http;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class ResultRegisterUserDto : ResultDto
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string EmailValidationToken { get; set; }
    }
}
