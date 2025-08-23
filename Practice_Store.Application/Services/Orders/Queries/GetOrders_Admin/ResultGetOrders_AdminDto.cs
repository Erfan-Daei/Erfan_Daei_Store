namespace Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin
{
    public class ResultGetOrders_AdminDto
    {
        public List<GetOrders_AdminDto> OrdersList { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
