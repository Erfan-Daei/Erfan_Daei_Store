using System.ComponentModel.DataAnnotations;

namespace Endpoint.Api.Model.Registeration
{
    public class SignUpDto
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
    }
}
