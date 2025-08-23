namespace Practice_Store.Application.Services.Products.Queries.GetProductList_Admin
{
    public class ProductList_AdminDto
    {
        public long Id { get; set; }
        public string ProductImageSrc { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Displayed { get; set; }
        public int Price { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public byte OffPercentage { get; set; }
        public float ProductScore { get; set; }
        public int ProductReviewCount { get; set; }
    }
}
