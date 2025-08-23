namespace Endpoint.Api.Model.UserManagement
{
    public class ChangePassword_ConfirmationTokenDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
