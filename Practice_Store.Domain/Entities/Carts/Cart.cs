using Practice_Store.Domain.Entities.Commons;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Domain.Entities.Carts
{
    public class Cart : BaseEntity
    {
        public virtual IdtUser User { get; set; }
        public string? UserId { get; set; }

        public Guid BrowserId { get; set; }

        public bool IsDone { get; set; } = false;

        public ICollection<CartProduct>? CartProducts { get; set; }
    }
}
