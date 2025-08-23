namespace Endpoint.Api.Areas.Admin.Model.ProductManagement
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool Displayed { get; set; }
        public long CategoryId { get; set; }
        public List<string> ImageSrc { get; set; }
        public List<ApiProductSizeDto> Sizes { get; set; }
        public byte OffPercentage { get; set; }
    }
}
