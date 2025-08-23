using Practice_Store.Common;

namespace Practice_Store.Application.Services.Common.GetProductMenu
{
    public interface IGetProductMenu
    {
        ResultDto<List<GetProductMenuDto>> Execute();
    }
}
