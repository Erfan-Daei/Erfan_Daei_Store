using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.DeleteProduct
{
    public interface IDeleteProduct
    {
        ResultDto Execute(long Id);
    }
}
