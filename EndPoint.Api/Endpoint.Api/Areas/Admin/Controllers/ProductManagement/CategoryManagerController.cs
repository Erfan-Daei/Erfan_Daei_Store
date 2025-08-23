using Endpoint.Api.Areas.Admin.Model.ProductManagement;
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
    public class CategoryManagerController : ControllerBase
    {
        private readonly IProductFacad _productFacad;
        public CategoryManagerController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }

        [HttpGet("Id")]
        public IActionResult GET(long? ParentId)
        {
            var Result = _productFacad.GetCategoriesService.Execute(ParentId);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Categories = Result.Data,
                Links = new List<Link>()
                {
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "CategoryManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "CategoryUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "CategoryManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewCategory",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "CategoryManager", new { Area = "Admin", Id = "Id" }, Request.Scheme) ?? "",
                        Rel = "CategoryDelete",
                        Method = "DELETE"
                    },
                }

            };

            return Ok(Output);
        }

        [HttpPost]
        public IActionResult POST([FromBody] CategoryDto _Request)
        {
            var Result = _productFacad.AddCategoryService.Execute(_Request.Id, _Request.Name);

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
                        Href = Url.Action(nameof(GET), "CategoryManager", new { Area = "Admin", ParentId = Result.Data }, Request.Scheme) ?? "",
                        Rel = "CategoryDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(PUT), "CategoryManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "CategoryUpdate",
                        Method = "PUT"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "CategoryManager", new { Area = "Admin", Id = Result.Data }, Request.Scheme) ?? "",
                        Rel = "CategoryDelete",
                        Method = "DELETE"
                    },
                }
            };

            string Uri = Url.Action(nameof(GET), "CategoryManager", new { Area = "Admin", ParentId = Result.Data }, Request.Scheme) ?? "";

            return Created(Uri, Output);
        }

        [HttpPut]
        public IActionResult PUT([FromBody] CategoryDto _Request)
        {
            var Result = _productFacad.EditCategoryService.Execute((int)_Request.Id, _Request.Name);

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
                        Href = Url.Action(nameof(GET), "CategoryManager", new { Area = "Admin", ParentId = _Request.Id }, Request.Scheme) ?? "",
                        Rel = "CategoryDetail",
                        Method = "GET"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(POST), "CategoryManager", new { Area = "Admin" }, Request.Scheme) ?? "",
                        Rel = "NewCategory",
                        Method = "POST"
                    },
                    new Link()
                    {
                        Href = Url.Action(nameof(DELETE), "CategoryManager", new { Area = "Admin", Id = _Request.Id }, Request.Scheme) ?? "",
                        Rel = "CategoryDelete",
                        Method = "DELETE"
                    },
                }
            };

            return Ok(Output);
        }

        [HttpDelete("Id")]
        public IActionResult DELETE(long Id)
        {
            var Result = _productFacad.DeleteCategoryService.Execute(Id);

            if (!Result.IsSuccess)
            {
                return Problem(Result.Message, "", Convert.ToInt16(Result.StatusCode));
            }

            dynamic Output = new
            {
                Message = Result.Message,
                StatusCode = Result.StatusCode,
            };

            return Ok(Output);
        }
    }
}
