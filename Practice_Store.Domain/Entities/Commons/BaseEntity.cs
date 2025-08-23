namespace Practice_Store.Domain.Entities.Commons
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime InsertTime { get; set; } = System.DateTime.UtcNow;
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedTime { get; set; }
    }
    public partial class BaseEntity : BaseEntity<long>
    {

    }
}
