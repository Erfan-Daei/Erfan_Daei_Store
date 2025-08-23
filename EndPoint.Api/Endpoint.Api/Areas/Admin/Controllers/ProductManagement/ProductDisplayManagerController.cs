using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Common;

namespace Endpoint.Api.Areas.Admin.Controllers.ProductManagement
{
    [Route("api/Area/Admin/ProductManager/[controller]")]
    [Area("Admin")]
    [Authorize(Policy = "ProductManagementAdmins")]
    [ApiController]
    public class ProductDisplayManagerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        public ProductDisplayManagerController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpPut("Id")]
        public IActionResult PUT(long Id)
        {
            var Result = _productFacad.ChangeProductDisplayedService.Execute(Id);

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action("GET", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action("POST", "ProductManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewProduct",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action("PUT", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action("DELETE", "ProductManager", new { Area = "Admin", Id = Id }, Request.Scheme) ?? "",
                        Rel = "ProductDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }
    }
}
