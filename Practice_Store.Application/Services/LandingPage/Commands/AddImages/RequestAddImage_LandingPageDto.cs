using Microsoft.AspNetCore.Http;
using Practice_Store.Domain.Entities.LandingPage;

namespace Practice_Store.Application.Services.LandingPage.Commands.AddImages
{
    public class RequestAddImage_LandingPageDto
    {
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public LandingPageImageLocation ImageLocation { get; set; }
    }
}
