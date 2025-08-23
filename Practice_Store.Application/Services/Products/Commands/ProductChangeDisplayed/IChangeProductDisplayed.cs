using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.ProductChangeDisplayed
{
    public interface IChangeProductDisplayed
    {
        ResultDto Execute(long Id);
    }
}
