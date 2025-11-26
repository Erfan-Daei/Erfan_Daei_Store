using Practice_Store.Common;
using System.ComponentModel.DataAnnotations;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConPassword { get; set; }
        public List<string> Roles { get; set; } = [UserRoles.Customer];
    }
}
