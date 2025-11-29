using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class AddProductRepo : IAddproductRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public AddProductRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddProduct(Product product)
        {
            try
            {
                _databaseContext.Products.Add(product);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddOff(ProductOff off)
        {
            try
            {
                _databaseContext.ProductOffs.Add(off);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddImages(List<ProductImages> productImages)
        {
            try
            {
                _databaseContext.ProductImages.AddRange(productImages);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddSizes(IEnumerable<ProductSizes> sizes)
        {
            try
            {
                _databaseContext.ProductSizes.AddRange(sizes);
                _databaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
