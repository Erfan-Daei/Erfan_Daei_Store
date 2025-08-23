using Microsoft.AspNetCore.Hosting;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System.Text.RegularExpressions;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Application.Services.Products.Commands.AddProduct
{
    public class AddProductService : IAddProduct
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AddProductService(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultDto<long> Execute(RequestAddProductDto Request)
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Name))
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا نام محصول را وارد کنید",
                        Status_Code = Status_Code.BAD_REQUEST
                    };
                }
                if (string.IsNullOrEmpty(Request.Brand))
                {
                    return new ResultDto<long>()
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
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا قیمت را وارد کنید\nقیمت نمیتواند از حروف تشکیل شود",
                        Status_Code = Status_Code.BAD_REQUEST
                    };
                }
                if (Request.CategoryId == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا دسته بندی محصول را انتخاب کنید",
                        Status_Code = Status_Code.BAD_REQUEST
                    };      
                }
                if (Request.Images.Count == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا حداقل یک عکس را برای محصول انتخاب کنید",
                        Status_Code = Status_Code.BAD_REQUEST
                    };
                }
                if (Request.Sizes.Count == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا حداقل یک سایز را برای محصول انتخاب کنید",
                        Status_Code = Status_Code.BAD_REQUEST
                    };
                }
                var _Category = _databaseContext.Categories.Find(Request.CategoryId);

                Product Product = new Product()
                {
                    Name = Request.Name,
                    Brand = Request.Brand,
                    Description = Request.Description,
                    Price = Request.Price,
                    Displayed = Request.Displayed,
                    Category = _Category,
                };
                _databaseContext.Products.Add(Product);

                ProductOff Off = new ProductOff()
                {
                    Product = Product,
                    Percentage = Request.OffPercentage
                };
                _databaseContext.ProductOffs.Add(Off);

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
                        Product = Product,
                        Src = UploadResult.FileNameAddress,
                    });
                }
                _databaseContext.ProductImages.AddRange(ProductImages);

                List<ProductSizes> ProductSizes = new List<ProductSizes>();
                foreach (var item in Request.Sizes)
                {
                    ProductSizes.Add(new ProductSizes
                    {
                        Product = Product,
                        Size = item.Size,
                        Inventory = item.Inventory,
                    });
                }
                _databaseContext.ProductSizes.AddRange(ProductSizes);

                _databaseContext.SaveChanges();
                return new ResultDto<long>()
                {
                    Data = Product.Id,
                    IsSuccess = true,
                    Message = "محصول با موفقیت ثبت شد",
                    Status_Code = Status_Code.CREATED,
                };
            }
            catch (Exception)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "ثبت ناموفق",
                    Status_Code = Status_Code.INTERNAL_SERVER_ERROR,
                };
            }
        }
    }
}
