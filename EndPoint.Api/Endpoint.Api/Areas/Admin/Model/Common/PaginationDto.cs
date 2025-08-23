namespace Endpoint.Api.Areas.Admin.Model.Common
{
    public class PaginationDto
    {
        public string? SearchKey { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
