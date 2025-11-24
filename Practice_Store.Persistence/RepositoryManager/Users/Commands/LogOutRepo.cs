using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class LogOutRepo : ILogOutRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public LogOutRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public bool RemoveUserToken(string UserId)
        {
            try
            {
                var Token = _databaseContext.UserTokens.Where(t => t.UserId == UserId && t.Name == nameof(TokenType.AccessToken)).ToList();
                _databaseContext.UserTokens.RemoveRange(Token);
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
