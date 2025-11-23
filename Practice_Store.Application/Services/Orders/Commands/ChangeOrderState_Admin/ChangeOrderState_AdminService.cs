using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.ChangeOrderState_Admin
{
    public class ChangeOrderState_AdminService : IChangeOrderState_Admin
    {
        private readonly IChangeOrderState_AdminRepo _changeOrderState_AdminRepo;
        public ChangeOrderState_AdminService(IChangeOrderState_AdminRepo changeOrderState_AdminRepo)
        {
            _changeOrderState_AdminRepo = changeOrderState_AdminRepo;
        }

        public ResultDto Execute(long OrderId, OrderState OrderState)
        {
            var _Order = _changeOrderState_AdminRepo.GetOrder(OrderId);

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
                TimeSpan CheckWeek = System.DateTime.UtcNow - _Order.InsertTime;
                if (CheckWeek.TotalDays <= 7)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "باید حداقل 7 روز از ثبت سفارش گذشته باشد",
                        StatusCode = StatusCodes.Status400BadRequest,
                    };
                }
            }

            switch (OrderState)
            {
                case OrderState.Processing:
                    _Order.OrderState = OrderState.Processing;
                    break;
                case OrderState.AdminCanceled:
                    _Order.OrderState = OrderState.AdminCanceled;
                    break;
                case OrderState.Posted:
                    _Order.OrderState = OrderState.Posted;
                    break;
                case OrderState.Delivered:
                    _Order.OrderState = OrderState.Delivered;
                    break;
                default:
                    break;
            }
            _changeOrderState_AdminRepo.Save();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}