using Microsoft.AspNetCore.Http;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Common;

namespace Practice_Store.Application.Services.Products.Commands.DeleteProduct
{
    public class DeleteProductService : IDeleteProduct
    {
        private readonly IDatabaseContext _databaseContext;
        public DeleteProductService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(long Id)
        {
            var _Product = _databaseContext.Products.Find(Id);

            if (_Product == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد",
                    StatusCode = StatusCodes.Status404NotFound,
                };
            }

            var ImageList = _databaseContext.ProductImages.Where(p => p.ProductId == Id);
            foreach (var image in ImageList)
            {
                _databaseContext.ProductImages.Remove(image);
                File.Delete("G:\\Practice_Store\\EndPoint.Site\\wwwroot\\" + image.Src);
            }

            var SizeList = _databaseContext.ProductSizes.Where(p => p.ProductId == Id);
            foreach (var size in SizeList)
            {
                _databaseContext.ProductSizes.Remove(size);
            }

            var Off = _databaseContext.ProductOffs.Where(p => p.ProductId == Id).FirstOrDefault();
            _databaseContext.ProductOffs.Remove(Off);

            _databaseContext.Products.Remove(_Product);
            _databaseContext.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "موفق",
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
