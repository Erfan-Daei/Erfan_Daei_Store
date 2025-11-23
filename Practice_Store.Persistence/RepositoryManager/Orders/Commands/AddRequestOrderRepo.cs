using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Orders.Commands;
using Practice_Store.Domain.Entities.Orders;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Orders.Commands
{
    public class AddRequestOrderRepo : IAddRequestOrderRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public AddRequestOrderRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IdtUser? FindUser(string UserId)
        {
            return _databaseContext.Users.Find(UserId);
        }

        public bool AddOrderRequest(OrderRequest orderRequest)
        {
            try
            {
                _databaseContext.OrderRequests.Add(orderRequest);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddExtraInfo(OrderRequestExtraInfo orderRequestExtraInfo)
        {
            try
            {
                _databaseContext.OrderRequestExtraInfos.Add(orderRequestExtraInfo);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            _databaseContext.SaveChanges();
        }
    }
}
