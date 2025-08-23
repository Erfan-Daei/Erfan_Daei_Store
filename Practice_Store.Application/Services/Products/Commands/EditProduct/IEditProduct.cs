using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProduct
    {
        ResultDto Execute(RequestEditProductDto Request);
    }
}
