namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public class RequestGetProductList_AdminDto
    {
        public string? SearchKey { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
