using Practice_Store.Common;

namespace Practice_Store.Application.Services.Carts.Queries.GetCart
{
    public interface IGetCart
    {
        ResultDto<CartDto> GetCart(Guid BrowserId, string? UserId);
    }
}
