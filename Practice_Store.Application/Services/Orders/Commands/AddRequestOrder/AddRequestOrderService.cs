using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Application.Interfaces.ZarinPal;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Orders;

namespace Practice_Store.Application.Services.Orders.Commands.RequestOrder
{
    public class AddRequestOrderService : IAddRequestOreder
    {
        private readonly IAddRequestOrderRepo _addRequestOrderRepo;
        private readonly IManageZarinPal _manageZarinPal;
        public AddRequestOrderService(IAddRequestOrderRepo addRequestOrderRepo,
            IManageZarinPal manageZarinPal)
        {
            _addRequestOrderRepo = addRequestOrderRepo;
            _manageZarinPal = manageZarinPal;
        }

        public ResultDto<ResultAddRequestOrder> Execute(RequestAddRequestOrderDto Request)
        {
            var _User = _addRequestOrderRepo.FindUser(Request.UserId);

            if (_User.Name == "کاربر")
            {
                _User.Name = Request.Name;
            }

            OrderRequest orderRequest = new OrderRequest()
            {
                Guid = Guid.NewGuid(),
                IsPayed = false,
                TotalSum = Request.TotalSum,
                User = _User,
                Shipping = Request.Shipping,
            };
            var AddOrderRequest = _addRequestOrderRepo.AddOrderRequest(orderRequest);

            OrderRequestExtraInfo extraInfo = new OrderRequestExtraInfo()
            {
                Name = Request.Name,
                LastName = Request.LastName,
                Address = Request.Address,
                PostCode = Request.PostCode,
                Mobile = Request.Mobile,
                OrderRequest = orderRequest,
            };
            var AddExtraInfo = _addRequestOrderRepo.AddExtraInfo(extraInfo);
            orderRequest.OrderRequestExtraInfo = extraInfo;
            _addRequestOrderRepo.Save();

            var RequestToZarinPal = _manageZarinPal.RequestToZarinPal(new RequestToZarinPalDto
            {
                Amount = orderRequest.TotalSum + Request.Shipping,
                OrderRequestGuid = orderRequest.Guid,
                Shipping = Request.Shipping
            });

            return new ResultDto<ResultAddRequestOrder>()
            {
                Data = new ResultAddRequestOrder()
                {
                    Guid = orderRequest.Guid,
                    Email = _User.Email,
                    Mobile = _User.PhoneNumber,
                    TotalSum = orderRequest.TotalSum,
                    OrderId = orderRequest.Id,
                    Authority = RequestToZarinPal.Result.Authority,
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status201Created,
            };
        }
    }
}
