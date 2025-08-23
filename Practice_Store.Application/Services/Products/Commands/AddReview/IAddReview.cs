using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.AddReview
{
    public interface IAddReview
    {
        ResultDto Execute(RequestAddReview Request);
    }
}
