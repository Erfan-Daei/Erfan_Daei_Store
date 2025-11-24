using Practice_Store.Application.Interfaces.Contexts;
using Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands;
using Practice_Store.Common;
using Practice_Store.Domain.Entities.Users;

namespace Practice_Store.Persistence.RepositoryManager.Users.Commands
{
    public class RefreshTokenRepo : IRefreshTokenRepo
    {
        private readonly IDatabaseContext _databaseContext;
        public RefreshTokenRepo(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IdtUsertokens? ChechRefreshToken(string refreshToken)
        {
            return _databaseContext.UserTokens
                .FirstOrDefault(x => x.RefreshToken == HashHelper.Hash(refreshToken));
        }

        public bool RemovePreviousToken(string UserId)
        {
            try
            {
                var PreviousToken = _databaseContext.UserTokens
                    .Where(t => t.Name == nameof(TokenType.AccessToken) && t.UserId == UserId)
                    .ToList();
                _databaseContext.UserTokens.RemoveRange(PreviousToken);
                _databaseContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddToken(IdtUsertokens Token)
        {
            try
            {
                _databaseContext.UserTokens.Add(Token);
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
