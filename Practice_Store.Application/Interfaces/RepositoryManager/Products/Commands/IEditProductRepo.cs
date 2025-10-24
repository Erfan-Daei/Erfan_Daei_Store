using Microsoft.AspNetCore.Http;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IEditProductRepo
    {
        ProductOff? FindOff(long productId);

        bool AddOff(ProductOff off);

        Dictionary<string, ProductSizes>? GetPastSizes(long productId);

        bool AddNewSizes(List<ProductSizes> sizes);

        bool RemovePastSizes(List<ProductSizes> sizes);

        bool RemovePastImage(long productId);

        bool AddNewImages(Product product, List<IFormFile> images, string _Name);
    }
}
