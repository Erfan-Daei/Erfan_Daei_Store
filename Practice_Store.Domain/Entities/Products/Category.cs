using Practice_Store.Domain.Entities.Commons;

namespace Practice_Store.Domain.Entities.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public long? ParentCategoryId { get; set; }

        public virtual ICollection<Category>? SubCategories { get; set; }
    }
}
