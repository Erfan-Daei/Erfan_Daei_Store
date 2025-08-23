using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_User
{
    public class ChangeOrderState_UserService : IChangeOrderState_User
    {
        private readonly IDatabaseContext _databaseContext;
        public ChangeOrderState_UserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(string UserId, long OrderId, OrderState OrderState)
        {
            var _Order = _databaseContext.Orders.FirstOrDefault(p => p.Id == OrderId &&
            p.UserId == UserId.ToString() &&
            (p.OrderState != OrderState.AdminCanceled && p.OrderState != OrderState.UserCanceled && p.OrderState != OrderState.Delivered));

            if (_Order == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "این سفارش امکان تغییر ندارد",
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }

            if (OrderState == OrderState.Delivered)
            {
                _Order.OrderState = OrderState.Delivered;
                _databaseContext.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "موفق",
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            else if (OrderState == OrderState.UserCanceled)
            {
                TimeSpan Check48Hours = System.DateTime.UtcNow - _Order.InsertTime;
                if (Check48Hours.TotalHours <= 48)
                {
                    _Order.OrderState = OrderState.UserCanceled;
                    _databaseContext.SaveChanges();
                    return new ResultDto()
                    {
                        IsSuccess = true,
                        Message = "موفق",
                        StatusCode = StatusCodes.Status200OK,
                    };
                }
                else
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "مهلت لغو سفارش تمام شده است",
                        StatusCode = StatusCodes.Status403Forbidden,
                    };
                }
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطایی رخ داد لطفا مجدد اقدام کنید",
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}
