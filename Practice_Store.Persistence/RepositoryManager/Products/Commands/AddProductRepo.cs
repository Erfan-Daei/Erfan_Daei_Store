using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class AddProductRepo : IAddproductRepo
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddProductRepo(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
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

        public bool AddImages(Product product, List<IFormFile> images, string _Name)
        {
            try
            {
                List<ProductImages> ProductImages = new List<ProductImages>();
                foreach (var item in images)
                {
                    var UploadResult = UploadImageFile(new RequestUploadImageFile
                    {
                        File = item,
                        Name = _Name,
                        _hostingEnvironment = _hostingEnvironment,
                        FolderPath = $@"images\ProductImages\",
                    });
                    ProductImages.Add(new ProductImages()
                    {
                        Product = product,
                        Src = UploadResult.FileNameAddress,
                    });
                }
                _databaseContext.ProductImages.AddRange(ProductImages);
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
