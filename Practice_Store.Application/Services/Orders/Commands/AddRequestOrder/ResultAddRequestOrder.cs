namespace Practice_Store.Application.Services.Orders.Commands.RequestOrder
{
    public class ResultAddRequestOrder
    {
        public Guid Guid { get; set; }
        public long OrderId { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int TotalSum { get; set; }
    }
}
