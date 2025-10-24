using Microsoft.AspNetCore.Http;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IAddproductRepo
    {
        bool AddProduct(Product product);

        bool AddOff(ProductOff off);

        bool AddImages(Product product, List<IFormFile> images, string _Name);

        bool AddSizes(IEnumerable<ProductSizes> sizes);
    }
}
