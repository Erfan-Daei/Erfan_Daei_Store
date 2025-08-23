using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.DeleteCategory
{
    public interface IDeleteCategory
    {
        ResultDto Execute(long Id);
    }
}
