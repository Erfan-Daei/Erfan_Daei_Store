using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Queries.GetAllSubCategories
{
    public class GetAllSubCategoriesService : IGetAllSubCategories
    {
        private readonly IGetAllSubCategoriesRepo _getAllSubCategoriesRepo;
        public GetAllSubCategoriesService(IGetAllSubCategoriesRepo getAllSubCategoriesRepo)
        {
            _getAllSubCategoriesRepo = getAllSubCategoriesRepo;
        }
        public ResultDto<List<GetAllCategoriesDto>> Execute()
        {
            var _Categories = _getAllSubCategoriesRepo.GetAllCategories()
                .Select(p => new GetAllCategoriesDto
                {
                    Id = p.Id,
                    Name = $"{p.ParentCategory.Name}  -  {p.Name}",
                }).ToList();

            _Categories.Add(new GetAllCategoriesDto
            {
                Id = 0,
                Name = "یک گزینه را انتخاب کنید"
            });
            return new ResultDto<List<GetAllCategoriesDto>>
            {
                Data = _Categories,
                IsSuccess = true,
                Status_Code = Status_Code.OK,
            };
        }
    }
}
