using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.AddProduct
{
    public interface IAddProduct
    {
        ResultDto<long> Execute(RequestAddProductDto Request);
    }
}
