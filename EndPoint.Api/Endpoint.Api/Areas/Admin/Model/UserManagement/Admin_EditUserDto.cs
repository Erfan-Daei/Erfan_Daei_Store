namespace Endpoint.Api.Areas.Admin.Model.UserManagement
{
    public class Admin_EditUserDto
    {
        public string UserId { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConPassword { get; set; }

    }
}
