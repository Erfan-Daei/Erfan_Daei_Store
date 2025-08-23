namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Site
{
    public class ResultGetProductList_SiteDto
    {
        public List<GetProductList_SiteDto> ProductList { get; set; }
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
