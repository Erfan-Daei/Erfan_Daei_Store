using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Password { get; set; }
        public string ConPassword { get; set; }
        public List<string> Roles { get; set; } = [UserRoles.Customer];
    }
}
