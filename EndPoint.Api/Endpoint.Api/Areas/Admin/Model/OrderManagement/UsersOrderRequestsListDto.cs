namespace Endpoint.Api.Areas.Admin.Model.OrderManagement
{
    public class UsersOrderRequestsListDto
    {
        public bool? IsPayed { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 20;
    }
}
