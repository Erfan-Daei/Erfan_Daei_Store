using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Products.Commands;
using Practice_Store.Domain.Entities.Products;

namespace Practice_Store.Persistence.RepositoryManager.Products.Commands
{
    public class ChangeProductDisplayRepo : IChangeProductDisplayRepo
    {
        private readonly IDatabaseContext _databaseContext;

        public ChangeProductDisplayRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool ChangeDisplay(Product product)
        {
            try
            {
                product.Displayed = !product.Displayed;
                _databaseContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
