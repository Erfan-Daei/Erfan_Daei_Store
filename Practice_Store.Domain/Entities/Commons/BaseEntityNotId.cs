namespace Practice_Store.Domain.Entities.Commons
{
    public abstract class BaseEntityNotId
    {
        public DateTime InsertTime { get; set; } = System.DateTime.UtcNow;
        public DateTime? UpdateTime { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateTime { get; set; }
    }
}
