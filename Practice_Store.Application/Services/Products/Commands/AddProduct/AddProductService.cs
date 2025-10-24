using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Products.Commands.AddProduct
{
    public class AddProductService : IAddProduct
    {
        private readonly IProductRepoFinders _productRepoFinders;
        private readonly IAddproductRepo _addProductRepo;
        public AddProductService(IProductRepoFinders productRepoFinders,
            IAddproductRepo addProductRepo)
        {
            _productRepoFinders = productRepoFinders;
            _addProductRepo = addProductRepo;
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
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (string.IsNullOrEmpty(Request.Brand))
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا برند محصول را وارد کنید",
                        StatusCode = StatusCodes.Status400BadRequest
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
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (Request.CategoryId == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا دسته بندی محصول را انتخاب کنید",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (Request.Images.Count == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا حداقل یک عکس را برای محصول انتخاب کنید",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                if (Request.Sizes.Count == 0)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "لطفا حداقل یک سایز را برای محصول انتخاب کنید",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                var _Category = _productRepoFinders.FindCategory(Request.CategoryId);

                Product Product = new Product()
                {
                    Name = Request.Name,
                    Brand = Request.Brand,
                    Description = Request.Description,
                    Price = Request.Price,
                    Displayed = Request.Displayed,
                    Category = _Category,
                };
                var AddProduct = _addProductRepo.AddProduct(Product);
                if (!AddProduct)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "مشکل سور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                ProductOff Off = new ProductOff()
                {
                    Product = Product,
                    Percentage = Request.OffPercentage
                };
                var AddOff = _addProductRepo.AddOff(Off);
                if (!AddOff)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "مشکل سور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var AddImage = _addProductRepo.AddImages(Product, Request.Images, Request.Name);
                if (!AddImage)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "مشکل سور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

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
                var AddSize = _addProductRepo.AddSizes(ProductSizes);
                if (!AddSize)
                {
                    return new ResultDto<long>()
                    {
                        IsSuccess = false,
                        Message = "مشکل سور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                return new ResultDto<long>()
                {
                    Data = Product.Id,
                    IsSuccess = true,
                    Message = "محصول با موفقیت ثبت شد",
                    StatusCode = StatusCodes.Status201Created,
                };
            }
            catch (Exception)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "ثبت ناموفق",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
