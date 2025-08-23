namespace Endpoint.Api.Areas.Admin.Model.UserManagement
{
    public class Admin_UserDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string? Mobile { get; set; }
        public bool MobileConfirmed { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string IsActive { get; set; }
        public List<string> Roles { get; set; }
    }
}
