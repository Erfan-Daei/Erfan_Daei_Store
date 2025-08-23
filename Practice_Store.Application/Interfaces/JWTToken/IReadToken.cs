using System.Security.Claims;

namespace Practice_Store.Application.Interfaces.JWTToken
{
    public interface IReadToken
    {
        public string GetUserId(ClaimsPrincipal User);
        public string GetUserEmail(ClaimsPrincipal User);
        public List<string> GetuserRoles(ClaimsPrincipal User);
    }
}
