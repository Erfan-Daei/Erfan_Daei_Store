using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
using Practice_Store.Domain.Entities.Products;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class EditProductRepo : IEditProductRepo
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EditProductRepo(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public bool AddNewImages(Product product, List<IFormFile> images, string _Name)
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
                foreach (var image in PastImages)
                {
                    _databaseContext.ProductImages.Remove(image);
                    File.Delete(_hostingEnvironment.WebRootPath + "\\" + image.Src);
                }
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
    }
}
