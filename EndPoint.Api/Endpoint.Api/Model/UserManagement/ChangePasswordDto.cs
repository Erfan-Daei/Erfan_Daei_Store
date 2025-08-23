namespace Endpoint.Api.Model.UserManagement
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string ConPassword { get; set; }
    }
}
