using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Services.LandingPage.Commands.EditImages
{
    public class EditImage_LandingPageService : IEditImage_LandingPage
    {
        private readonly IEditImages_LandingPageRepo _editImages_LandingPageRepo;
        public EditImage_LandingPageService(IEditImages_LandingPageRepo editImages_LandingPageRepo)
        {
            _editImages_LandingPageRepo = editImages_LandingPageRepo;
        }

        public ResultDto Execute(RequestEditImage_LandingPageDto Request)
        {
            var _Image = _editImages_LandingPageRepo.FindImage(Request.Id);
            if (_Image.ImageLocation != Request.ImageLocation)
            {
                int count = 0;
                switch (_Image.ImageLocation)
                {
                    case LandingPageImageLocation.Slider:
                        count = 1;
                        break;
                    case LandingPageImageLocation.Row_1:
                        count = 3;
                        break;
                    case LandingPageImageLocation.Row_2:
                        count = 1;
                        break;
                    case LandingPageImageLocation.Row_3:
                        count = 2;
                        break;
                    case LandingPageImageLocation.LastLeft:
                        count = 1;
                        break;
                    default:
                        break;
                }
                var AllImages = _editImages_LandingPageRepo.FindAllImages();
                if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)0).Count() == 1 && Request.ImageLocation == (LandingPageImageLocation)0)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تعداد تصاویر اسلایدر نمیتواند بیشتر از 1 باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)1).Count() == 3 && Request.ImageLocation == (LandingPageImageLocation)1)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تعداد تصاویر این ردیف نمیتواند بیشتر از 3 باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)2).Count() == 1 && Request.ImageLocation == (LandingPageImageLocation)2)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تعداد تصاویر این ردیف نمیتواند بیشتر از 1 باشد",
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
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                if (AllImages.Where(p => p.ImageLocation == (LandingPageImageLocation)_Image.ImageLocation).Count() == count)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = $"تعداد تصاویر این ردیف نمیتواند کم تر از {count} باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }
            _Image.Src = Request.ImageSrc;
            _Image.ImageLocation = Request.ImageLocation;
            _Image.Title = Request.Title;
            _Image.Link = Request.Link;

            _editImages_LandingPageRepo.Save();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
