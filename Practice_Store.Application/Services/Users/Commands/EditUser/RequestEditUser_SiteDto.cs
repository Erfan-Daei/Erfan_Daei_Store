namespace Practice_Store.Application.Services.Users.Commands.EditUser
{
    public class RequestEditUser_SiteDto
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public string? Mobile { get; set; }
    }
}
