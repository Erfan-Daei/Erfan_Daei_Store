using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Services.LandingPage.Commands.DeleteImage
{
    public class DeleteImage_LandingPageService : IDeleteImage_LandingPage
    {
        private readonly IDatabaseContext _databaseContext;
        public DeleteImage_LandingPageService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(long Id)
        {
            var Image = _databaseContext.LandingPageImages.Find(Id);
            if (Image == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تصویر یافت نشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            var AllImages = _databaseContext.LandingPageImages.ToList();
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)0).Count() == 1 && Image.ImageLocation == (LandingPageImageLocation)0)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر اسلایدر نمیتواند از 1 کمتر باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)1).Count() == 3 && Image.ImageLocation == (LandingPageImageLocation)1)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند از 3 کمتر باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)2).Count() == 1 && Image.ImageLocation == (LandingPageImageLocation)2)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند از 1 کمتر باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)3).Count() == 2 && Image.ImageLocation == (LandingPageImageLocation)3)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند از 2 کمتر باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)4).Count() == 1 && Image.ImageLocation == (LandingPageImageLocation)4)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند از 1 کمتر باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            _databaseContext.LandingPageImages.Remove(Image);
            File.Delete("G:\\Practice_Store\\EndPoint.Site\\wwwroot\\" + Image.Src);
            _databaseContext.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status204NoContent,
            };
        }
    }
}
