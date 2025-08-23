using Microsoft.AspNetCore.Http;

namespace Practice_Store.Application.Interfaces.Cookie
{
    public interface IManageCookie
    {
        public Guid GetBrowserId(HttpContext context);
        public string GetValue(HttpContext context, string token);
        public void Add(HttpContext context, string token, string value);
    }
}
