using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class EditProductRepo : IEditProductRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public EditProductRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddNewImages(List<ProductImages> productImages)
        {
            try
            {
                _databaseContext.ProductImages.AddRange(productImages);
                _databaseContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddNewSizes(List<ProductSizes> sizes)
        {
            try
            {
                _databaseContext.ProductSizes.AddRange(sizes);
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

        public ProductOff? FindOff(long productId)
        {
            return _databaseContext.ProductOffs.FirstOrDefault(p => p.ProductId == productId);
        }

        public Dictionary<string, ProductSizes>? GetPastSizes(long productId)
        {
            return _databaseContext.ProductSizes.Where(p => p.ProductId == productId).ToDictionary(p => p.Size);
        }

        public bool RemovePastImage(long productId)
        {
            try
            {
                var PastImages = _databaseContext.ProductImages.Where(p => p.ProductId == productId);
                _databaseContext.ProductImages.RemoveRange(PastImages);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemovePastSizes(List<ProductSizes> sizes)
        {
            try
            {
                _databaseContext.ProductSizes.RemoveRange(sizes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SaveChanges()
        {
            _databaseContext.SaveChanges();
        }
    }
}
