using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class SaveTokenRepo : ISaveTokenRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public SaveTokenRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool AddToken(IdtUsertokens usertokens)
        {
            try
            {
                _databaseContext.UserTokens.Add(usertokens);
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
