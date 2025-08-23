using Practice_Store.Application.Services.Products.Queries.GetProductList_Site;

namespace Endpoint.Api.Model.ProductManagement
{
    public class GetProductListDto
    {
        public string SearchKey { get; set; }
        public Ordering Ordering { get; set; } = (Ordering)0;
        public int CategoryId { get; set; } = 0;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
