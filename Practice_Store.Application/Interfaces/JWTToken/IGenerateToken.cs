using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.JWTToken
{
    public interface IGenerateToken
    {
        (string,string) GenerateToken(string UserId, string UserEmail, List<string> UserRoles);
        IdtUsertokens GenerateIdtUserToken(string UserId, string Value, string RefreshToken);
    }
}
