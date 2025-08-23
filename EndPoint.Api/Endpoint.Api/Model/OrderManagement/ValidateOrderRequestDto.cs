namespace Endpoint.Api.Model.OrderManagement
{
    public class ValidateOrderRequestDto
    {
        public Guid Guid {  get; set; }
        public int Shipping {  get; set; }
        public string authority { get; set; }
        public string status { get; set; }
    }
}
