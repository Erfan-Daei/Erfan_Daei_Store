using Microsoft.AspNetCore.Hosting;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System.Text.RegularExpressions;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Application.Services.Products.Commands.EditProduct
{
    public class EditProductService : IEditProduct
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EditProductService(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultDto Execute(RequestEditProductDto Request)
        {
            if (string.IsNullOrEmpty(Request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا نام محصول را وارد کنید",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }
            if (string.IsNullOrEmpty(Request.Brand))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا برند محصول را وارد کنید",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }
            if (string.IsNullOrEmpty(Request.Description))
            {
                Request.Description = "-";
            }

            var PricePattern = @"^\d+$";
            if (Request.Price == 0 || !Regex.Match(Request.Price.ToString(), PricePattern).Success)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا قیمت را وارد کنید\nقیمت نمیتواند از حروف تشکیل شود",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }
            if (Request.CategoryId == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا دسته بندی محصول را انتخاب کنید",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }
            if (Request.Images.Count == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا حداقل یک عکس را برای محصول انتخاب کنید",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }
            if (Request.Sizes.Count == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا حداقل یک سایز را برای محصول انتخاب کنید",
                    Status_Code = Status_Code.BAD_REQUEST
                };
            }

            var _Product = _databaseContext.Products.Find(Request.Id);
            var _Category = _databaseContext.Categories.Find(Request.CategoryId);
            var _Off = _databaseContext.ProductOffs.Where(p => p.ProductId == Request.Id).FirstOrDefault();

            _Product.Name = Request.Name;
            _Product.Brand = Request.Brand;
            _Product.Description = Request.Description;
            _Product.Price = Request.Price;
            _Product.Category = _Category;
            _Product.Displayed = Request.Displayed;

            if (_Off == null)
            {
                ProductOff Off = new ProductOff()
                {
                    Product = _Product,
                    Percentage = Request.OffPercentage,
                };
                _databaseContext.ProductOffs.Add(Off);
            }
            else
            {
                _Off.Percentage = Request.OffPercentage;
            }

            var PastSizes = _databaseContext.ProductSizes.Where(p => p.ProductId == Request.Id).ToDictionary(p => p.Size);

            List<ProductSizes> ProductSizes = new List<ProductSizes>();
            foreach (var item in Request.Sizes)
            {
                if (PastSizes.ContainsKey(item.Size))
                {
                    PastSizes[item.Size].Inventory = item.Inventory;
                }
                else
                {
                    ProductSizes.Add(new ProductSizes
                    {
                        Product = _Product,
                        Size = item.Size,
                        Inventory = item.Inventory,
                    });
                }
            }
            _databaseContext.ProductSizes.AddRange(ProductSizes);
            var UpdatePastSizes = PastSizes.Values.ToList();
            var RemoveSizes = UpdatePastSizes.Where(p => !Request.Sizes.Any(n => n.Size == p.Size)).ToList();
            foreach (var item in RemoveSizes)
            {
                _databaseContext.ProductSizes.Remove(item);
            }


            var PastImages = _databaseContext.ProductImages.Where(p => p.ProductId == Request.Id);
            foreach (var image in PastImages)
            {
                _databaseContext.ProductImages.Remove(image);
                File.Delete(_hostingEnvironment.WebRootPath + "\\" + image.Src);
            }
            List<ProductImages> ProductImages = new List<ProductImages>();
            foreach (var item in Request.Images)
            {
                var UploadResult = UploadImageFile(new RequestUploadImageFile
                {
                    File = item,
                    Name = Request.Name,
                    _hostingEnvironment = _hostingEnvironment,
                    FolderPath = $@"images\ProductImages\",
                });
                ProductImages.Add(new ProductImages()
                {
                    Product = _Product,
                    Src = UploadResult.FileNameAddress,
                });
            }
            _databaseContext.ProductImages.AddRange(ProductImages);

            _databaseContext.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "محصول با موفقیت ویرایش شد",
                Status_Code = Status_Code.OK,
            };
        }
    }
}
