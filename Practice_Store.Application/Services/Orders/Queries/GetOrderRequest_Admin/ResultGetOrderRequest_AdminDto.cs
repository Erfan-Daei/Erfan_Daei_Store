namespace Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin
{
    public class ResultGetOrderRequest_AdminDto
    {
        public List<GetOrderRequest_AdminDto> OrderRequestList { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
