using Microsoft.AspNetCore.Hosting;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.DeleteImage;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site;

namespace Practice_Store.Application.ServiceCollection
{
    public class LandingPageFacad : ILandingPageFacad
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _hostingEnvironment;
        public LandingPageFacad(IDatabaseContext databaseContext, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = databaseContext;
            _hostingEnvironment = hostingEnvironment;
        }

        private IAddImage_LandingPage _addImageLandingPage;
        public IAddImage_LandingPage AddImage_LandingPageService
        {
            get
            {
                return _addImageLandingPage = _addImageLandingPage ?? new AddImage_LandingPageService(_databaseContext, _hostingEnvironment);
            }
        }

        private IGetImages_Site _getImages_Site;
        public IGetImages_Site GetImages_SiteService
        {
            get
            {
                return _getImages_Site = _getImages_Site ?? new GetImages_SiteService(_databaseContext);
            }
        }

        private IEditImage_LandingPage _editImage;
        public IEditImage_LandingPage EditImageService
        {
            get
            {
                return _editImage = _editImage ?? new EditImage_LandingPageService(_databaseContext, _hostingEnvironment);
            }
        }

        private IDeleteImage_LandingPage _deleteImage_LandingPage;
        public IDeleteImage_LandingPage DeleteImage_LandingPageService
        {
            get
            {
                return _deleteImage_LandingPage = _deleteImage_LandingPage ?? new DeleteImage_LandingPageService(_databaseContext);
            }
        }
    }
}
