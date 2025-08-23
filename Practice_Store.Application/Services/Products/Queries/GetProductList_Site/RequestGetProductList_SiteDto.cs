namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Site
{
    public class RequestGetProductList_SiteDto
    {
        public int CategoryId { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; }
    }
}
