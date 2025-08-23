using Microsoft.AspNetCore.Http;
using Practice_Store.Common;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Users.Commands.EditUser
{
    public static class EditUser_AdminValidation
    {
        public static ResultDto Validate(RequestEditUser_SiteDto Request)
        {
            //Word Input Check
            if (!string.IsNullOrEmpty(Request.Name))
            {
                string NamePattern = @"^[a-zA-Z\u0600-\u06FF\s]+$";
                var MatchName = Regex.Match(Request.Name, NamePattern);
                if (!MatchName.Success)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام و نام خانوادگی نمی تواند از اعداد و حروف خاص تشکیل شود",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            if (!string.IsNullOrEmpty(Request.LastName))
            {
                string NamePattern = @"^[a-zA-Z\u0600-\u06FF\s]+$";
                var MatchName = Regex.Match(Request.LastName, NamePattern);
                if (!MatchName.Success)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "نام و نام خانوادگی نمی تواند از اعداد و حروف خاص تشکیل شود",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            //Number Input Check
            if (!string.IsNullOrEmpty(Request.PostCode))
            {
                if (!Regex.IsMatch(Request.PostCode, @"^\d{10}$"))
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "کدپستی نمی تواند از حروف تشکیل شود - کدپستی باید 10 رقم باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }
            if (!string.IsNullOrEmpty(Request.Mobile.ToString()))
            {
                Match moblie = Regex.Match(Request.Mobile.ToString(), @"^(?:0|98)?(\d{10})$");
                if (!moblie.Success)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "تلفن نمی تواند از حروف تشکیل شود",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            return new ResultDto()
            {
                IsSuccess = true,
            };
        }
    }
}
