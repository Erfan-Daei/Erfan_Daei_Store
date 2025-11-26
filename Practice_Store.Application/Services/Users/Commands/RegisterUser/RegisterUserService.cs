using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUser
    {
        private readonly IUserRepoFinder _userRepoFinder;
        private readonly IRegisterUserRepo _registerUserRepo;
        public RegisterUserService(IUserRepoFinder userRepoFinder,
            IRegisterUserRepo registerUserRepo)
        {
            _userRepoFinder = userRepoFinder;
            _registerUserRepo = registerUserRepo;
        }

        public ResultRegisterUserDto ValidateUser(RequestRegisterUserDto Request)
        {
            try
            {
                var GetEmail = _userRepoFinder.EmailExist(Request.Email);
                if (GetEmail != null)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "این پست الکترونیک قبلا استفاده شده است",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }

                foreach (var role in Request.Roles)
                {
                    var CheckRole = _registerUserRepo.FindRole(role);
                    if (CheckRole == null)
                    {
                        return new ResultRegisterUserDto
                        {
                            IsSuccess = false,
                            Message = "نقش یافت نشد",
                            StatusCode = StatusCodes.Status400BadRequest
                        };
                    }
                }

                IdtUser User = new IdtUser
                {
                    Name = Request.Name,
                    LastName = Request.LastName,
                    Email = Request.Email,
                    Address = Request.Address,
                    PostCode = Request.PostCode,
                    EmailConfirmed = false,
                    InsertTime = DateTime.UtcNow,
                    UserName = Request.Email
                };
                var Result = _registerUserRepo.CreateUser(User, Request.Password);

                if (!Result.Succeeded)
                {
                    string ErrorMessage = "";
                    foreach (var Error in Result.Errors.ToList())
                        ErrorMessage += Error.Description + Environment.NewLine;

                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = ErrorMessage,
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                ;

                var Acivate = _registerUserRepo.ActivateUser(User);

                var AddRole = _registerUserRepo.AddToRole(User, Request.Roles);
                if (!AddRole.Succeeded)
                {
                    var _user = _userRepoFinder.FindUserById(User.Id);
                    if (_user != null)
                    {
                        var Delete = _registerUserRepo.DeleteUser(User);
                    }
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "ثبت نام ناموفق !!!",
                        StatusCode = StatusCodes.Status500InternalServerError
                    };
                }

                var Token = _registerUserRepo.GenerateEmailConfirmationToken(User);
                return new ResultRegisterUserDto
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ثبت شد",
                    StatusCode = StatusCodes.Status202Accepted,
                    UserId = User.Id,
                    UserEmail = User.Email,
                    EmailValidationToken = Token
                };
            }

            catch (Exception)
            {
                var _user = _userRepoFinder.FindUserByEmail(Request.Email);
                if (_user != null)
                {
                    var Delete = _registerUserRepo.DeleteUser(_user);
                }
                return new ResultRegisterUserDto
                {
                    IsSuccess = false,
                    Message = "ثبت نام ناموفق !!!",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }

        public ResultRegisterUserDto ValidateEmail(string UserId, string EmailValidationToken)
        {
            try
            {
                var User = _userRepoFinder.FindUserById(UserId);
                if (User == null)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "کاربر یافت نشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
                var ConfEmail = _registerUserRepo.ConfirmEmail(User, EmailValidationToken);
                if (!ConfEmail.Succeeded)
                {
                    return new ResultRegisterUserDto
                    {
                        IsSuccess = false,
                        Message = "کاربر تایید نشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }

                return new ResultRegisterUserDto
                {
                    IsSuccess = true,
                    Message = "ایمیل شما با موفقیت تایید شد",
                    StatusCode = StatusCodes.Status200OK,
                    UserId = User.Id,
                };
            }
            catch (Exception)
            {
                return new ResultRegisterUserDto
                {
                    IsSuccess = false,
                    Message = "ثبت نام ناموفق !!!",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
