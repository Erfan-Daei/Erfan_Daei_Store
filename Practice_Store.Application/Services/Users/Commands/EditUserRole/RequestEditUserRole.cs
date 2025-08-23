namespace Practice_Store.Application.Services.Users.Commands.EditUserRole
{
    public class RequestEditUserRole
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
