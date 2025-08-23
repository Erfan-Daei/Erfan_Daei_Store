using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.LandingPage;
using static Practice_Store.Common.UploadFile;

namespace Practice_Store.Application.Services.LandingPage.Commands.EditImages
{
    public class EditImage_LandingPageService : IEditImage_LandingPage
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EditImage_LandingPageService(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultDto Execute(RequestEditImage_LandingPageDto Request)
        {
            var _Image = _databaseContext.LandingPageImages.Find(Request.Id);
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
                var AllImages = _databaseContext.LandingPageImages.ToList();
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

            if (_Image == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "تصویر یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
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

            if (Request.Image != null)
            {
                var ImageUrl = UploadImageFile(new RequestUploadImageFile
                {
                    File = Request.Image,
                    Name = Request.ImageLocation.ToString(),
                    _hostingEnvironment = _hostingEnvironment,
                    FolderPath = $@"images\LandingPageImages\",
                });
                File.Delete("G:\\Practice_Store\\EndPoint.Site\\wwwroot\\" + _Image.Src);
                _Image.Src = ImageUrl.FileNameAddress;
            }

            _Image.ImageLocation = Request.ImageLocation;
            _Image.Title = Request.Title;
            _Image.Link = Request.Link;

            _databaseContext.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
