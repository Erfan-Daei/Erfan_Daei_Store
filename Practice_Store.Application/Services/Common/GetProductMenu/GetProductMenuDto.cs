namespace Practice_Store.Application.Services.Common.GetProductMenu
{
    public class GetProductMenuDto
    {
        public long CategoryId { get; set; }
        public string? Name { get; set; }
        public List<GetProductMenuDto>? ChildCategories { get; set; }
    }
}
