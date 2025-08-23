using System.ComponentModel.DataAnnotations;

namespace Endpoint.Api.Areas.Admin.Model.UserManagement
{
    public class Admin_SignUpDto
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConPassword { get; set; }

        public List<string>? Roles { get; set; }
    }
}
