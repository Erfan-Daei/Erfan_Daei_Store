using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace EndPoint.Site.Filters
{
    public class RolesExceptCustomerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                if (userRoles.Contains("Customer") && userRoles.Count == 1)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
