using System.Security.Claims;

namespace EndPoint.Site.Utilities
{
    public static class ClaimUtility
    {
        public static string? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                return userId;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static List<string> GetRoles(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                List<string> Roles = new List<string>();
                foreach (var item in claimsIdentity.Claims.Where(p => p.Type.EndsWith("role")))
                {
                    Roles.Add(item.Value);
                }
                return Roles;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string GetEmail(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                string Email = claimsIdentity?.FindFirst(ClaimTypes.Email)?.Value ?? null;
                return Email;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
