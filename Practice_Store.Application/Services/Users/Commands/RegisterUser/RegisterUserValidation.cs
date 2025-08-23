using Microsoft.AspNetCore.Http;
using Practice_Store.Common;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserValidation
    {
        public static ResultDto CheckValidation(RequestRegisterUserDto Request)
        {
            //Email Check
            if (string.IsNullOrEmpty(Request.Email))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا پست الکترونیک را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            string EmailPattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";
            var MatchEmail = Regex.Match(Request.Email, EmailPattern);
            if (!MatchEmail.Success)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا پست الکترونیک را به درستی وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            //Password Check
            if (string.IsNullOrEmpty(Request.Password))
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "لطفا رمزعبور را وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            if (Request.Password != Request.ConPassword)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "رمزعبور و تکرار آن برابر نیست",
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            string PasswordPattern = @"^(?=.*\b\w+\b){8,}(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])(?=.*[!@#$%^&*(),.?"":{}|<>]).+$";
            var MatchPassword = Regex.Match(Request.Password, PasswordPattern);
            if (!MatchPassword.Success)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "رمز عبور باید حداقل شامل 8 حرف، یک حرف بزرگ، یک حرف کوچک، یک عدد و یک حرف خاص باشد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            //Word Input Check
            if (!string.IsNullOrEmpty(Request.Name))
            {
                string NamePattern = @"^[a-zA-Z\u0600-\u06FF\s]+$";
                var MatchName = Regex.Match(Request.Name, NamePattern);
                if (!MatchName.Success)
                {
                    return new ResultDto
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
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "نام و نام خانوادگی نمی تواند از اعداد و حروف خاص تشکیل شود",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            //Number Input Check
            if (!string.IsNullOrEmpty(Request.PostCode.ToString()))
            {
                var PostCodePattern = @"^\d+$";
                var MatchPostCode = Regex.Match(Request.PostCode.ToString(), PostCodePattern);
                if (!MatchPostCode.Success)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "کدپستی نمی تواند از حروف تشکیل شود",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            return new ResultDto
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
