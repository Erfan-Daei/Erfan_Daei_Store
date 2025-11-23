using Practice_Store.Application.Interfaces.FacadPatterns;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Commands;
using Practice_Store.Application.Interfaces.RepositoryManager.Carts.Quesries;
using Practice_Store.Application.Services.Carts.Commands.AddToCart;
using Practice_Store.Application.Services.Carts.Commands.RemoveFromCart;
using Practice_Store.Application.Services.Carts.Queries.GetCart;

namespace Practice_Store.Application.ServiceCollection
{
    public class CartFacad : ICartFacad
    {
        private readonly IAddToCartRepo _addToCartRepo;
        private readonly IRemoveFromCartRepo _removeFromCartRepo;
        private readonly IGetCartRepo _getCartRepo;
        public CartFacad(IAddToCartRepo addToCartRepo,
            IRemoveFromCartRepo removeFromCartRepo,
            IGetCartRepo getCartRepo)
        {
            _addToCartRepo = addToCartRepo;
            _removeFromCartRepo = removeFromCartRepo;
            _getCartRepo = getCartRepo;
        }

        private IAddToCart _addToCart;
        public IAddToCart AddToCartService
        {
            get
            {
                return _addToCart = _addToCart ?? new AddToCartService(_addToCartRepo);
            }
        }

        private IRemoveFromCart _removeFromCart;
        public IRemoveFromCart RemoveFromCartService
        {
            get
            {
                return _removeFromCart = _removeFromCart ?? new RemoveFromCartService(_removeFromCartRepo);
            }
        }

        private IGetCart _getCart;
        public IGetCart GetCartService
        {
            get
            {
                return _getCart = _getCart ?? new GetCartService(_getCartRepo);
            }
        }
    }
}
