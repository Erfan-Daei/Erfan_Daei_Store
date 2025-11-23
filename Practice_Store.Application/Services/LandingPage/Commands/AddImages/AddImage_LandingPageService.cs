using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.LandingPage;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Application.Services.LandingPage.Commands.AddImages
{
    public class AddImage_LandingPageService : IAddImage_LandingPage
    {
        private readonly IAddImage_LandingPageRepo _addImageRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AddImage_LandingPageService(IAddImage_LandingPageRepo addImageRepo, IHostingEnvironment hostingEnvironment)
        {
            _addImageRepo = addImageRepo;
            _hostingEnvironment = hostingEnvironment;
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

            if (Request.Image == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا عکس را انتخاب کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (Request.Title == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا عنوان را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (Request.Link == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا لینک را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (Request.Image == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "لطفا لینک را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var ResultUploadImage = UploadImageFile(new RequestUploadImageFile
            {
                File = Request.Image,
                Name = Request.ImageLocation.ToString(),
                _hostingEnvironment = _hostingEnvironment,
                FolderPath = $@"images\LandingPageImages\",
            });

            LandingPageImages LandingPageImage = new LandingPageImages()
            {
                Link = Request.Link,
                Title = Request.Title,
                ImageLocation = Request.ImageLocation,
                Src = ResultUploadImage.FileNameAddress,
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
