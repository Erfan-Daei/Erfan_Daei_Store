namespace Practice_Store.Application.Interfaces.RepositoryManager.Users.Commands
{
    public interface ILogOutRepo
    {
        bool RemoveUserToken(string UserId);
    }
}
