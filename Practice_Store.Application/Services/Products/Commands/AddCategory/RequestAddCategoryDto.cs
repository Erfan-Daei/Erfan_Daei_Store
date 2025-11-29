namespace Practice_Store.Application.Services.Products.Commands.AddCategory
{
    public class RequestAddCategoryDto
    {
        public long? ParentId { get; set; }
        public string Name { get; set; }
    }
}
