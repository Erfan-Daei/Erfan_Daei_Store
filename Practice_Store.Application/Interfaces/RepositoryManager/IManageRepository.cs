namespace Practice_Store.Application.Interfaces.RepositoryManager
{
    public interface IManageRepository
    {
        T? FindByStringId<T>(string Id) where T : class;
        T? FindByLongId<T>(long Id) where T : class;
        int InsertToRepository<T>(T entity) where T : class;
        int UpdateRepository<T>(T entity) where T : class;
        int DeleteFromRepository<T>(T entity) where T : class;
    }
}
