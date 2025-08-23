using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.AddCategory
{
    public interface IAddCategory
    {
        ResultDto<long> Execute(long? ParentId, string Name);
    }
}
