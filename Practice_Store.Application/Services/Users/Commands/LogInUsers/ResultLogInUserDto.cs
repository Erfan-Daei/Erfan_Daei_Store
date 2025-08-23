namespace Practice_Store.Application.Services.Users.Commands.LogInUsers
{
    public class ResultLogInUserDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
