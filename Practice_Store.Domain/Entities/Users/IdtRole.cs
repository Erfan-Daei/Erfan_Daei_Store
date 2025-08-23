using Microsoft.AspNetCore.Identity;

namespace Practice_Store.Domain.Entities.Users
{
    public class IdtRole : IdentityRole
    {
        public DateTime InsertTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedTime { get; set; }
    }
}
