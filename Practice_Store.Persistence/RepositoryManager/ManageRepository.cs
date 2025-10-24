using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager;

namespace Practice_Store.Persistence.RepositoryManager
{
    public class ManageRepository : IManageRepository
    {
        private readonly IDatabaseContext _databaseContext;
        public ManageRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public T? FindByStringId<T>(string Id) where T : class
        {
            return _databaseContext.Set<T>().Find(Id);
        }
        public T? FindByLongId<T>(long Id) where T : class
        {
            return _databaseContext.Set<T>().Find(Id);
        }
        public int InsertToRepository<T>(T entity) where T : class
        {
            _databaseContext.Set<T>().Add(entity);
            return _databaseContext.SaveChanges();
        }
        public int UpdateRepository<T>(T entity) where T : class
        {
            _databaseContext.Set<T>().Update(entity);
            return _databaseContext.SaveChanges();
        }
        public int DeleteFromRepository<T>(T entity) where T : class
        {
            _databaseContext.Set<T>().Remove(entity);
            return _databaseContext.SaveChanges();
        }
    }
}
