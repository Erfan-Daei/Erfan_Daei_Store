using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategory
    {
        ResultDto Execute(long Id, string Name);
    }
}
