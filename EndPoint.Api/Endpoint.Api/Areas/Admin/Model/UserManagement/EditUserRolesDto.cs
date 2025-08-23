namespace Endpoint.Api.Areas.Admin.Model.UserManagement
{
    public class EditUserRolesDto
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
