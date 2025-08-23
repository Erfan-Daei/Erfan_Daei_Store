using Microsoft.AspNetCore.Identity;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Domain.Entities.Users
{
    public class IdtUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public DateTime InsertTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedTime { get; set; }
    }
}
