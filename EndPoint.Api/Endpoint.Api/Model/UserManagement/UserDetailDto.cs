namespace Endpoint.Api.Model.UserManagement
{
    public class UserDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string? Mobile { get; set; }
        public string Email { get; set; }
    }
}
