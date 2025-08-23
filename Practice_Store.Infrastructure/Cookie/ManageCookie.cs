using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Cookie;

namespace Practice_Store.Infrastructure.Cookie
{
    public class ManageCookie : IManageCookie
    {
        public Guid GetBrowserId(HttpContext context)
        {
            string BrowserId = GetValue(context, "BrowserId");
            if (string.IsNullOrWhiteSpace(BrowserId))
            {
                string Value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", Value);
                BrowserId = Value;
            }
            Guid.TryParse(BrowserId, out Guid BrowserGuid);
            return BrowserGuid;
        }

        public string? GetValue(HttpContext context, string token)
        {
            string cookieValue;
            if (!context.Request.Cookies.TryGetValue(token, out cookieValue))
            {
                return null;
            }
            return cookieValue;
        }

        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }

        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.UtcNow.AddDays(7),
            };
        }
    }
}
