namespace Practice_Store.Application.JWTToken
{
    public interface IGenerateToken
    {
        public (string,string) GenerateToken(string UserId, string UserEmail, List<string> UserRoles);
    }
}
