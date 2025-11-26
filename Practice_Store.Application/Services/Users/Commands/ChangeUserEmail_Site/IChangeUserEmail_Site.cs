namespace Practice_Store.Application.Services.Users.Commands.ChangeUserEmail_Site
{
    public interface IChangeUserEmail_Site
    {
        ResultChangeUserEmail_SiteDto CheckEmailValidation(RequestChangeUserEmail_SiteDto Request);
    }
}
