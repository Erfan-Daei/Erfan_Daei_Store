using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands
{
    public interface IAddproductRepo
    {
        bool AddProduct(Product product);

        bool AddOff(ProductOff off);

        bool AddImages(List<ProductImages> productImages);

        bool AddSizes(IEnumerable<ProductSizes> sizes);
    }
}
