using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Services.LandingPage.Commands.AddImages
{
    public class AddImage_LandingPageService : IAddImage_LandingPage
    {
        private readonly IAddImage_LandingPageRepo _addImageRepo;

        public AddImage_LandingPageService(IAddImage_LandingPageRepo addImageRepo)
        {
            _addImageRepo = addImageRepo;
        }

        public ResultDto Execute(RequestAddImage_LandingPageDto Request)
        {

            var AllImages = _addImageRepo.GetAllImages();
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)1).Count() == 3 && Request.ImageLocation == (LandingPageImageLocation)1)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند بیشتر از 3 باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)3).Count() == 2 && Request.ImageLocation == (LandingPageImageLocation)3)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند بیشتر از 2 باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)4).Count() == 1 && Request.ImageLocation == (LandingPageImageLocation)4)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تعداد تصاویر این ردیف نمیتواند بیشتر از 1 باشد",

                };
            }

            LandingPageImages LandingPageImage = new LandingPageImages()
            {
                Link = Request.Link,
                Title = Request.Title,
                ImageLocation = Request.ImageLocation,
                Src = Request.ImageSrc,
            };
            var Add = _addImageRepo.AddImages(LandingPageImage);
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
