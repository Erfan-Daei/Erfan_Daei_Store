using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Products;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

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
            var _Product = _productRepoFinders.FindProduct(Request.Id);
            var _Category = _productRepoFinders.FindCategory(Request.CategoryId);
            var _Off = _editProductRepo.FindOff(Request.Id);

            if (!string.IsNullOrEmpty(Request.Name))
            {
                _Product.Name = Request.Name;
            }
            if (!string.IsNullOrEmpty(Request.Brand))
            {
                _Product.Brand = Request.Brand;
            }
            if (!string.IsNullOrEmpty(Request.Description))
            {
                _Product.Description = Request.Description;
            }

            if (Request.Price != 0 && Request.Price.HasValue)
            {
                _Product.Price = Request.Price.Value;
            }
            if (Request.CategoryId != 0 && Request.CategoryId.HasValue)
            {
                _Product.CategoryId = Request.CategoryId.Value;
            }
            if (Request.Displayed.HasValue)
            {
                _Product.Displayed = Request.Displayed.Value;
            }

            if (_Off == null)
            {
                ProductOff Off = new ProductOff()
                {
                    Product = _Product,
                    Percentage = Request.OffPercentage ?? 0,
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
                _Off.Percentage = Request.OffPercentage ?? 0;
            }

            if (Request.Sizes.Count > 0 && Request.Sizes != null)
            {
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
            }

            if (Request.ImageSrc.Count > 0 && Request.ImageSrc != null)
            {
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
                var productImages = new List<ProductImages>();
                foreach (var image in Request.ImageSrc)
                {
                    productImages.Add(new ProductImages()
                    {
                        Product = _Product,
                        Src = image.Src,
                        UpdateTime = DateTime.Now,
                    });
                }
                var AddImages = _editProductRepo.AddNewImages(productImages);
                if (!AddImages)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مشکل سرور",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }
            }

            _editProductRepo.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "محصول با موفقیت ویرایش شد",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
