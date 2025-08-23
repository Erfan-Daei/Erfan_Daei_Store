using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Orders.Queries.GetOrderRequest_Admin
{
    public class GetOrderRequest_AdminService : IGetOrderRequest_Admin
    {
        private readonly IDatabaseContext _databaseContext;
        public GetOrderRequest_AdminService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultGetOrderRequest_AdminDto> Execute(RequestGetOrderRequest_AdminDto Request, int PageSize = 20)
        {
            var _OrderRequest = _databaseContext.OrderRequests.Include(p => p.OrderRequestExtraInfo).AsQueryable();

            if (Request.SearchKey == null && Request.IsPayed != null)
            {
                _OrderRequest = _OrderRequest.Where(p => p.IsPayed == Request.IsPayed).OrderByDescending(p => p.Id);
            }

            else if (Request.SearchKey != null && Request.IsPayed != null)
            {
                _OrderRequest = _OrderRequest.Where(p => p.IsPayed == Request.IsPayed &&
                (p.OrderRequestExtraInfo.Name.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.LastName.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.Address.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.PostCode.ToString().Contains(Request.SearchKey) ||
                    p.PayDate.ToString().Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.Mobile.ToString().Contains(Request.SearchKey) ||
                    p.Authority.ToString().Contains(Request.SearchKey) ||
                    p.UserId.ToString().Contains(Request.SearchKey) ||
                    p.RefId.ToString().Contains(Request.SearchKey))).OrderByDescending(p => p.Id);
            }
            else if (Request.SearchKey == null && Request.IsPayed == null)
            {
                _OrderRequest = _OrderRequest.OrderByDescending(p => p.Id);
            }
            else
            {
                _OrderRequest = _OrderRequest.Where(p => p.OrderRequestExtraInfo.Name.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.LastName.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.Address.Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.PostCode.ToString().Contains(Request.SearchKey) ||
                    p.PayDate.ToString().Contains(Request.SearchKey) ||
                    p.OrderRequestExtraInfo.Mobile.ToString().Contains(Request.SearchKey) ||
                    p.Authority.ToString().Contains(Request.SearchKey) ||
                    p.UserId.ToString().Contains(Request.SearchKey) ||
                    p.RefId.ToString().Contains(Request.SearchKey)).OrderByDescending(p => p.Id);
            }

            int rowsCount = _OrderRequest.Count();
            var OrderRequestList = _OrderRequest.ToPaged(Request.Page, PageSize).Select(p => new GetOrderRequest_AdminDto
            {
                UserId = p.UserId,
                TotalSum = p.TotalSum,
                Shipping = p.Shipping,
                IsPayed = p.IsPayed,
                PayDate = p.PayDate,
                Authority = p.Authority,
                RefId = p.RefId,
                Name = p.OrderRequestExtraInfo?.Name ?? "-",
                LastName = p.OrderRequestExtraInfo?.LastName ?? "-",
                Address = p.OrderRequestExtraInfo?.Address ?? "-",
                PostCode = p.OrderRequestExtraInfo?.PostCode ?? 0,
                Mobile = p.OrderRequestExtraInfo.Mobile,
            }).ToList();

            return new ResultDto<ResultGetOrderRequest_AdminDto>()
            {
                Data = new ResultGetOrderRequest_AdminDto
                {
                    OrderRequestList = OrderRequestList,
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
