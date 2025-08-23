namespace Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin
{
    public class GetOrderRequest_AdminDto
    {
        public string UserId { get; set; }
        public int TotalSum { get; set; }
        public int Shipping { get; set; }
        public bool IsPayed { get; set; } = false;
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; } = "Not Authorized";
        public long RefId { get; set; } = 0;

        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Mobile { get; set; }
    }
}
