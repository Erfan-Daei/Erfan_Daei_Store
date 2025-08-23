namespace Endpoint.Api.Areas.Admin.Model.AdminManagement
{
    public class EditAdminDto
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? Mobile { get; set; }
    }
}
