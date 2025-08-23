using Microsoft.AspNetCore.Http;

namespace Practice_Store.Application.Services.Products.Commands.EditProduct
{
    public class RequestEditProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool Displayed { get; set; }
        public long CategoryId { get; set; }
        public List<EditProductImageSrcDto> ImageSrc { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<EditProductSizeDto> Sizes { get; set; }
        public byte OffPercentage { get; set; }
    }
}
