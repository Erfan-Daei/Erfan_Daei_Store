using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Products.Commands.EditProduct
{
    public class EditProductService : IEditProduct
    {
        private readonly IEditProductRepo _editProductRepo;
        private readonly IProductRepoFinders _productRepoFinders;
        public EditProductService(IEditProductRepo editProductRepo,
            IProductRepoFinders productRepoFinders)
        {
            _editProductRepo = editProductRepo;
            _productRepoFinders = productRepoFinders;
        }

        public ResultDto Execute(RequestEditProductDto Request)
        {
            if (string.IsNullOrEmpty(Request.Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا نام محصول را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (string.IsNullOrEmpty(Request.Brand))
            {
                return new ResultDto()
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
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا قیمت را وارد کنید\nقیمت نمیتواند از حروف تشکیل شود",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (Request.CategoryId == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا دسته بندی محصول را انتخاب کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (Request.Images.Count == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا حداقل یک عکس را برای محصول انتخاب کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (Request.Sizes.Count == 0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا حداقل یک سایز را برای محصول انتخاب کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var _Product = _productRepoFinders.FindProduct(Request.Id);
            var _Category = _productRepoFinders.FindCategory(Request.CategoryId);
            var _Off = _editProductRepo.FindOff(Request.Id);

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
                var AddOff = _editProductRepo.AddOff(Off);
                if (!AddOff)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشکل سرور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }
            else
            {
                _Off.Percentage = Request.OffPercentage;
            }

            var PastSizes = _editProductRepo.GetPastSizes(Request.Id);

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
            var AddSizes = _editProductRepo.AddNewSizes(ProductSizes);
            if (!AddSizes)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            var UpdatePastSizes = PastSizes.Values.ToList();
            var RemoveSizes = UpdatePastSizes.Where(p => !Request.Sizes.Any(n => n.Size == p.Size)).ToList();
            var RemoveSize = _editProductRepo.RemovePastSizes(RemoveSizes);
            if (!RemoveSize)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            var RemoveImage = _editProductRepo.RemovePastImage(Request.Id);
            if (!RemoveImage)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            var AddImages = _editProductRepo.AddNewImages(_Product, Request.Images, Request.Name);
            if (!AddImages)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "مشکل سرور",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "محصول با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
