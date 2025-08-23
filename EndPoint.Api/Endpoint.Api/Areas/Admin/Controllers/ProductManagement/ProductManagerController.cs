using Endpoint.Api.Areas.Admin.Model.Common;
using Endpoint.Api.Areas.Admin.Model.ProductManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.Products.Commands.AddProduct;
using Practice_Store.Application.Services.Products.Commands.EditProduct;
using Practice_Store.Application.Services.Products.Queries.GetProductList_Admin;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.ProductManagement
{
    [Route("api/Area/Admin/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "ProductManagementAdmins")]
    [ApiController]
    public class ProductManagerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        public ProductManagerController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpGet]
        public IActionResult GET([FromQuery] PaginationDto _Request)
        {
            var Result = _productFacad.GetProductList_AdminService.Execute(new RequestGetProductList_AdminDto
            {
                Page = _Request.Page,
                SearchKey = _Request.SearchKey,
                PageSize = _Request.PageSize,
            });

            dynamic Output = new
            {
                ProductList = Result.Data.ProductList,
                Page = Result.Data.CurrentPage,
                PageSize = Result.Data.PageSize,
                RowsCount = Result.Data.RowsCount,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "ProductManager", new { Area = "Admin", Id = "Id" }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "ProductManager", new { Area = "Admin", Id = "Id" }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "ProductManager", new { Area = "Admin", Id = "Id" }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpGet("Id")]
        public IActionResult GET(long Id)
        {
            var Result = _productFacad.GetProductDetails_AdminService.Execute(Id);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                ProductDetail = Result.Data,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] AddProductDto _Request)
        {
            List<IFormFile> Images = new List<IFormFile>();
            foreach (var image in _Request.ImageSrc)
            {
                IFormFile FormFile = CreateIFormFile(image);
                Images.Add(FormFile);
            }

            var Result = _productFacad.AddProductService.Execute(new RequestAddProductDto
            {
                Brand = _Request.Brand,
                CategoryId = _Request.CategoryId,
                Description = _Request.Description,
                Displayed = _Request.Displayed,
                Images = Images,
                Name = _Request.Name,
                OffPercentage = _Request.OffPercentage,
                Price = _Request.Price,
                Sizes = _Request.Sizes.Select(p => new ProductSizeDto
                {
                    Size = p.Size,
                    Inventory = p.Inventory,
                }).ToList()
            });

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "ProductManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] EditProductDto _Request)
        {
            List<IFormFile> Images = new List<IFormFile>();
            foreach (var image in _Request.ImageSrc)
            {
                IFormFile FormFile = CreateIFormFile(image);
                Images.Add(FormFile);
            }

            var Result = _productFacad.EditProductService.Execute(new RequestEditProductDto
            {
                Brand = _Request.Brand,
                CategoryId = _Request.CategoryId,
                Description = _Request.Description,
                Displayed = _Request.Displayed,
                Id = _Request.Id,
                Images = Images,
                Name = _Request.Name,
                OffPercentage = _Request.OffPercentage,
                Price = _Request.Price,
                Sizes = _Request.Sizes.Select(p => new EditProductSizeDto
                {
                    Size = p.Size,
                    Inventory = p.Inventory,
                }).ToList()
            });

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "ProductManager", new { Area = "Admin", Id = _Request.Id }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "ProductManager", new { Area = "Admin", Id = _Request.Id }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "ProductManager", new { Area = "Admin", Id = _Request.Id }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpDelete("Id")]
        public IActionResult DELETE(long Id)
        {
            var Result = _productFacad.DeleteProductService.Execute(Id);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(GET), "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "ProductList",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                }
            };

            return Ok(Output);
        }

        private IFormFile CreateIFormFile(string path)
        {
            var fileName = Path.GetFileName(path);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var memoryStream = new MemoryStream();
            fileStream.CopyTo(memoryStream);
            fileStream.Close();
            memoryStream.Position = 0;
            return new FormFile(memoryStream, 0, memoryStream.Length, fileName, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
        }
    }
}
