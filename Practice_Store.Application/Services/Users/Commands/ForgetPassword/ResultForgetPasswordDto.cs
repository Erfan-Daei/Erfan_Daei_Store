using Practice_Store.Common;

namespace Practice_Store.Application.Services.Users.Commands.ForgetPassword
{
    public partial class ForgetPasswordService
    {
        public class ResultForgetPasswordDto : ResultDto
        {
            public string Token { get; set; }
            public string Email { get; set; }
        }
    }

}
