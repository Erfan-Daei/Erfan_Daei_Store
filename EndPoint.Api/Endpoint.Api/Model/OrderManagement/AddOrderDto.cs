namespace Endpoint.Api.Model.OrderManagement
{
    public class AddOrderDto
    {
        public long OrderRequestId { get; set; }
        public long CartId { get; set; }
        public long UserId { get; set; }

        public string Authority { get; set; }
        public long RefId { get; set; }

        public int Code { get; set; }
        public int Shipping { get; set; }
    }
}
