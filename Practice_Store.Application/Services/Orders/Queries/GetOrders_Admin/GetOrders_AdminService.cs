using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrders_Admin
{
    public class GetOrders_AdminService : IGetOrders_Admin
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrders_AdminService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public ResultDto<ResultGetOrders_AdminDto> Execute(RequestGetOrders_AdminDto Request, int PageSize = 20)
        {
            var _Order = _databaseContext.Orders.Include(p => p.OrderRequest).AsQueryable();

            if (Request.SearchKey == null && Request.OrderState != null)
            {
                _Order = _Order.Where(p => p.OrderState == Request.OrderState).OrderByDescending(p => p.Id);
            }

            else if (Request.SearchKey != null && Request.OrderState != null)
            {
                _Order = _Order.Where(p => p.OrderState == Request.OrderState &&
                (p.Name.Contains(Request.SearchKey) ||
                    p.LastName.Contains(Request.SearchKey) ||
                    p.Address.Contains(Request.SearchKey) ||
                    p.PostCode.ToString().Contains(Request.SearchKey) ||
                    p.InsertTime.ToString().Contains(Request.SearchKey) ||
                    p.Mobile.ToString().Contains(Request.SearchKey) ||
                    p.OrderRequest.RefId.ToString().Contains(Request.SearchKey))).OrderByDescending(p => p.Id);
            }
            else if (Request.SearchKey == null && Request.OrderState == null)
            {
                _Order = _Order.OrderByDescending(p => p.Id);
            }
            else
            {
                _Order = _Order.Where(p => p.Name.Contains(Request.SearchKey) ||
                    p.LastName.Contains(Request.SearchKey) ||
                    p.Address.Contains(Request.SearchKey) ||
                    p.PostCode.ToString().Contains(Request.SearchKey) ||
                    p.InsertTime.ToString().Contains(Request.SearchKey) ||
                    p.Mobile.ToString().Contains(Request.SearchKey) ||
                    p.OrderRequest.RefId.ToString().Contains(Request.SearchKey)).OrderByDescending(p => p.Id);
            }

            int rowsCount = _Order.Count();
            var OrderList = _Order.ToPaged(Request.Page, PageSize).Select(p => new GetOrders_AdminDto
            {
                OrderId = p.Id,
                UserId = p.UserId,
                OrderRequestId = p.OrderRequestId,
                RefId = p.OrderRequest.RefId,
                PayDateTime = p.OrderRequest.PayDate,
                Name = p.Name,
                LastName = p.LastName,
                Address = p.Address,
                PostCode = p.PostCode,
                Mobile = p.Mobile,
                Shipping = p.Shipping,
                TotalSum = p.TotalSum,
                OrderState = p.OrderState,
            }).ToList();

            return new ResultDto<ResultGetOrders_AdminDto>()
            {
                Data = new ResultGetOrders_AdminDto
                {
                    OrdersList = OrderList,
                    CurrentPage = Request.Page,
                    PageSize = PageSize,
                    RowsCount = rowsCount
                },
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
