using Practice_Store.Application.Services.Carts;
using Practice_Store.Application.Services.Users.Queries.GetUserDetail_Site;

namespace EndPoint.Site.Models.ViewModels.CheckOut
{
    public class CheckOutViewModel
    {
        public CartDto Cart { get; set; }
        public GetUserDetail_SiteDto UserDetail { get; set; }
        public int ShippingPrice { get; set; }
    }
}
