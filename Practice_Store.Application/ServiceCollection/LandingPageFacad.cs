using Microsoft.AspNetCore.Hosting;
using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.LandingPage.Queries;
using Practice_Store.Application.Services.LandingPage.Commands.AddImages;
using Practice_Store.Application.Services.LandingPage.Commands.DeleteImage;
using Practice_Store.Application.Services.LandingPage.Commands.EditImages;
using Practice_Store.Application.Services.LandingPage.Queries.GetImages_Site;

namespace Practice_Store.Application.ServiceCollection
{
    public class LandingPageFacad : ILandingPageFacad
    {
        private readonly IAddImage_LandingPageRepo _addImageRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDeleteImage_LandingPageRepo _deleteImage_LandingPageRepo;
        private readonly IEditImages_LandingPageRepo _editImages_LandingPageRepo;
        private readonly IGetImage_SiteRepo _getImage_SiteRepo;
        public LandingPageFacad(IAddImage_LandingPageRepo addImageRepo,
            IHostingEnvironment hostingEnvironment,
            IDeleteImage_LandingPageRepo deleteImage_LandingPageRepo,
            IEditImages_LandingPageRepo editImages_LandingPageRepo,
            IGetImage_SiteRepo getImage_SiteRepo)
        {
            _addImageRepo = addImageRepo;
            _hostingEnvironment = hostingEnvironment;
            _deleteImage_LandingPageRepo = deleteImage_LandingPageRepo;
            _editImages_LandingPageRepo = editImages_LandingPageRepo;
            _getImage_SiteRepo = getImage_SiteRepo;
        }

        private IAddImage_LandingPage _addImageLandingPage;
        public IAddImage_LandingPage AddImage_LandingPageService
        {
            get
            {
                return _addImageLandingPage = _addImageLandingPage ?? new AddImage_LandingPageService(_addImageRepo, _hostingEnvironment);
            }
        }

        private IGetImages_Site _getImages_Site;
        public IGetImages_Site GetImages_SiteService
        {
            get
            {
                return _getImages_Site = _getImages_Site ?? new GetImages_SiteService(_getImage_SiteRepo);
            }
        }

        private IEditImage_LandingPage _editImage;
        public IEditImage_LandingPage EditImageService
        {
            get
            {
                return _editImage = _editImage ?? new EditImage_LandingPageService(_editImages_LandingPageRepo, _hostingEnvironment);
            }
        }

        private IDeleteImage_LandingPage _deleteImage_LandingPage;
        public IDeleteImage_LandingPage DeleteImage_LandingPageService
        {
            get
            {
                return _deleteImage_LandingPage = _deleteImage_LandingPage ?? new DeleteImage_LandingPageService(_deleteImage_LandingPageRepo);
            }
        }
    }
}
