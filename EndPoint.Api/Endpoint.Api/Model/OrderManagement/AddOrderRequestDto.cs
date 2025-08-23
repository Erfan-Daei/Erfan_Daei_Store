namespace Endpoint.Api.Model.OrderManagement
{
    public class AddOrderRequestDto
    {
        public int TotalSum { get; set; }
        public int Shipping { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public long? PostCode { get; set; }
        public string Mobile { get; set; }
    }
}
