using Practice_Store.Application.Services.Products.Queries.GetProductDetails_Site;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Admin
{
    public class GetProductDetail_AdminDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Displayed { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }

        public byte OffPercentage { get; set; }

        public List<GetProductDetail_AdminImagesDto> Images { get; set; }
        public List<GetProductDetail_AdminSizesDto> Sizes { get; set; }
    }
}
