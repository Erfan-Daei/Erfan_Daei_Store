namespace Practice_Store.Application.Interfaces.ZarinPal
{
    public class RequestToZarinPalDto
    {
        public int Amount { get; set; }
        public int Shipping { get; set; }
        public Guid OrderRequestGuid { get; set; }
    }
}
