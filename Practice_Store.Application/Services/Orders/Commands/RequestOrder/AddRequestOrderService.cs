using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;
using System.Text.RegularExpressions;

namespace Practice_Store.Application.Services.Orders.Commands.RequestOrder
{
    public class AddRequestOrderService : IAddRequestOreder
    {
        private readonly IDatabaseContext _databaseContext;
        public AddRequestOrderService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultAddRequestOrder> Execute(RequestAddRequestOrder Request)
        {
            if (string.IsNullOrEmpty(Request.Name) || string.IsNullOrEmpty(Request.LastName) || (string.IsNullOrEmpty(Request.Address) && Request.PostCode == 0) || string.IsNullOrEmpty(Request.Mobile))
            {
                return new ResultDto<ResultAddRequestOrder>
                {
                    IsSuccess = false,
                    Message = "لطفا تمامی اطلاعات را وارد کنید - (آدرس و یا کد پستی باید وارد شود)",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            if (!Regex.IsMatch(Request.Name, @"^[\u0600-\u06FF\s]+$") || !Regex.IsMatch(Request.LastName, @"^[\u0600-\u06FF\s]+$"))
            {
                return new ResultDto<ResultAddRequestOrder>
                {
                    IsSuccess = false,
                    Message = "لطفا نام و نام خانوادگی را به درستی وارد کنید",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            var _User = _databaseContext.Users.Find(Request.UserId);

            if (_User.Name == "کاربر")
            {
                _User.Name = Request.Name;
            }

            if (!Regex.IsMatch(_User.LastName, @"^[\u0600-\u06FF\s]+$"))
            {
                _User.LastName = Request.LastName;
            }

            if (!string.IsNullOrEmpty(Request.Address) && _User.Address == "-")
            {
                if (Regex.IsMatch(Request.Address, @"^[\u0600-\u06FF\s]+$"))
                {
                    _User.Address = Request.Address;
                }
                else
                {
                    return new ResultDto<ResultAddRequestOrder>
                    {
                        IsSuccess = false,
                        Message = "لطفا آدرس را به درستی وارد کنید",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }
            if (Request.PostCode != 0 && _User.PostCode == 0)
            {
                if (Regex.IsMatch(Request.PostCode.ToString(), @"^\d{10}$"))
                {
                    _User.PostCode = Request.PostCode;
                }
                else
                {
                    return new ResultDto<ResultAddRequestOrder>
                    {
                        IsSuccess = false,
                        Message = "لطفا کد پستی را درست وارد  کنید - کد پستی باید شامل 10 رقم باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }
            if (Regex.IsMatch(Request.Mobile, @"^(?:0|98)?(\d{10})$"))
            {
                _User.PhoneNumber = Request.Mobile;
            }
            else
            {
                return new ResultDto<ResultAddRequestOrder>
                {
                    IsSuccess = false,
                    Message = "تلفن نمی تواند از حروف تشکیل شود",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            OrderRequest orderRequest = new OrderRequest()
            {
                Guid = Guid.NewGuid(),
                IsPayed = false,
                TotalSum = Request.TotalSum,
                User = _User,
                Shipping = Request.Shipping,
            };
            _databaseContext.OrderRequests.Add(orderRequest);

            OrderRequestExtraInfo extraInfo = new OrderRequestExtraInfo()
            {
                Name = Request.Name,
                LastName = Request.LastName,
                Address = Request.Address,
                PostCode = Request.PostCode,
                Mobile = Request.Mobile,
                OrderRequest = orderRequest,
            };
            _databaseContext.OrderRequestExtraInfos.Add(extraInfo);
            orderRequest.OrderRequestExtraInfo = extraInfo;
            _databaseContext.SaveChanges();
            return new ResultDto<ResultAddRequestOrder>()
            {
                Data = new ResultAddRequestOrder()
                {
                    Guid = orderRequest.Guid,
                    Email = _User.Email,
                    Mobile = _User.PhoneNumber,
                    TotalSum = orderRequest.TotalSum,
                    OrderId = orderRequest.Id,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
