namespace Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin
{
    public class RequestGetOrderRequest_AdminDto
    {
        public bool? IsPayed { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; }
    }
}
