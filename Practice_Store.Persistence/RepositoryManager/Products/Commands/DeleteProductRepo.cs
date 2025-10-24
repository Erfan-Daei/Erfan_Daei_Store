using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class DeleteProductRepo : IDeleteProductRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public DeleteProductRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool DeleteImages(long productId)
        {
            try
            {
                var images = _databaseContext.ProductImages.Where(p => p.ProductId == productId).ToList();
                foreach (var image in images)
                {
                    _databaseContext.ProductImages.Remove(image);
                    File.Delete(@"G:\Practice_Store\EndPoint.Site\wwwroot\" + image.Src);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSizes(long productId)
        {
            try
            {
                var sizes = _databaseContext.ProductSizes.Where(p => p.ProductId == productId).ToList();
                _databaseContext.ProductSizes.RemoveRange(sizes);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOff(long productId)
        {
            try
            {
                var productOff = _databaseContext.ProductOffs.FirstOrDefault(p => p.ProductId == productId);
                if (productOff != null)
                {
                    _databaseContext.ProductOffs.Remove(productOff);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteProduct(Product product)
        {
            try
            {
                _databaseContext.Products.Remove(product);
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
