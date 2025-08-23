namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public class ResultGetProductList_AdminDto
    {
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public int CurrentPage { get; set; }

        public List<ProductList_AdminDto> ProductList { get; set; }
    }
}
