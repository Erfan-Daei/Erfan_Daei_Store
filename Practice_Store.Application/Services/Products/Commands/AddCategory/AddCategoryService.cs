using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Products;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Products.Commands.AddCategory
{
    public class AddCategoryService : IAddCategory
    {
        private readonly IDatabaseContext _databaseContext;
        public AddCategoryService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<long> Execute(long? ParentId, string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "لطفا نام دسته بندی را وارد نمایید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            string NamePattern = @"^[\u0621-\u0628\u062A-\u063A\u0641-\u0642\u0644-\u0648\u064A\u067E\u0686\u0698\u06A9\u06AF\u06BE\u06CC\s]+$";
            var MatchName = Regex.Match(Name, NamePattern);
            if (!MatchName.Success)
            {
                return new ResultDto<long>()
                {
                    IsSuccess = false,
                    Message = "نام دسته بندی نمیتواند شامل اعداد و حروف انگلیسی باشد",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            Category Category = new Category()
            {
                Name = Name,
                ParentCategory = GetParent(ParentId)
            };

            _databaseContext.Categories.Add(Category);
            _databaseContext.SaveChanges();

            return new ResultDto<long>()
            {
                Data = Category.Id,
                IsSuccess = true,
                Message = "دسته بندی با موفقیت اضافه شد",
                StatusCode = StatusCodes.Status201Created,
            };

        }
        private Category GetParent(long? ParentId)
        {
            return _databaseContext.Categories.Find(ParentId);
        }
    }
}
