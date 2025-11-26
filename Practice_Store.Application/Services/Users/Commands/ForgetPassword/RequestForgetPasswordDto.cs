using System.ComponentModel.DataAnnotations;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public class RequestForgetPasswordDto
    {
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string ConPassword { get; set; }
    }
}
