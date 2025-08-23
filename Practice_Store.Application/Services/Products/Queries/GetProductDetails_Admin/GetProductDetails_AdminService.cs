using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Application.Services.Products.Queries.GetProductDetails_Admin
{
    public class GetProductDetails_AdminService : IGetProductDetails_Admin
    {
        private readonly IDatabaseContext _databaseContext;
        public GetProductDetails_AdminService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<GetProductDetail_AdminDto> Execute(long Id)
        {
            var _Product = _databaseContext.Products
                .Where(p => p.Id == Id)
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.Off)
                .Include(p => p.Reviews)
                .FirstOrDefault();

            if (_Product == null)
            {
                return new ResultDto<GetProductDetail_AdminDto>()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            return new ResultDto<GetProductDetail_AdminDto>()
            {
                Data = new GetProductDetail_AdminDto()
                {
                    Id = Id,
                    Name = _Product.Name,
                    Brand = _Product.Brand,
                    Description = _Product.Description,
                    Price = _Product.Price,
                    Displayed = _Product.Displayed == true ? "نمایش" : "عدم نمایش",
                    CategoryId = _Product.CategoryId,
                    CategoryName = GetCategory(_Product.Category),
                    OffPercentage = _Product.Off?.Percentage ?? 0,
                    Sizes = _Product.ProductSizes.ToList().Select(p => new GetProductDetail_AdminSizesDto
                    {
                        SizeId = p.Id,
                        SizeName = p.Size,
                        Inventory = p.Inventory,
                    }).ToList(),
                    Images = _Product.ProductImages.ToList().Select(p => new GetProductDetail_AdminImagesDto
                    {
                        ImageId = p.Id,
                        Src = p.Src,
                    }).ToList(),
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
        private string GetCategory(Category Category)
        {
            var ParentCategory = _databaseContext.Categories.Find(Category.ParentCategoryId);
            return ParentCategory.Name + " - " + Category.Name;
        }
    }
}
