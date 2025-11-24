using Microsoft.AspNetCore.Identity;
using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class LogInUserRepo : ILogInUserRepo
    {
        private readonly UserManager<IdtUser> _userManager;
        private readonly IDatabaseContext _databaseContext;
        public LogInUserRepo(UserManager<IdtUser> userManager,
            IDatabaseContext databaseContext)
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
        }

        public bool CheckPassword(IdtUser user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password).Result;
        }

        public bool DeletePreviousTokens(string UserId)
        {
            try
            {
                var PreviousToken = _databaseContext.UserTokens
                .Where(t => t.Name == nameof(TokenType.AccessToken) && t.UserId == UserId).ToList();
                _databaseContext.UserTokens.RemoveRange(PreviousToken);

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
