using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

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
                    Percentage = Request.OffPercentage ?? 0
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
                var productImages = new List<ProductImages>();
                foreach (var image in Request.ImageSrc)
                {
                    productImages.Add(new ProductImages()
                    {
                        Product = Product,
                        Src = image.Src
                    });
                }
                var AddImage = _addProductRepo.AddImages(productImages);
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
