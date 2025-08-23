namespace Practice_Store.Application.Services.Orders.Commands.AddOrder
{
    public class RequestAddOrder
    {
        public long OrderRequestId { get; set; }
        public long CartId { get; set; }
        public string UserId { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; }
    }
}
