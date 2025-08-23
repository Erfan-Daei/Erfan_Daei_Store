using Practice_Store.Application.Interfaces.JWTToken;
using System.Security.Claims;

namespace Practice_Store.Infrastructure.JWTToken
{
    public class ReadToken : IReadToken
    {
        public string GetUserEmail(ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.Email).Value;
        }

        public string GetUserId(ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.Sid).Value;
        }

        public List<string> GetuserRoles(ClaimsPrincipal User)
        {
            return User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        }
    }
}
