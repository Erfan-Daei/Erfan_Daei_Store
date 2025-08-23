using Microsoft.AspNetCore.Identity;

namespace Practice_Store.Domain.Entities.Users
{
    public class IdtUsertokens : IdentityUserToken<string>
    {
        public Guid TokenId { get; set; }
        public DateTime TokenExpireDate { get; set; }
        public string RefreshToken {  get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}
