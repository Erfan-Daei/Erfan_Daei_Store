namespace Practice_Store.Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUsersDTO
    {
        public int PageSize { get; set; }
        public int RowsCount { get; set; }
        public int CurrentPage { get; set; }

        public List<UserWithRoles> UsersDtos { get; set; }
    }
}
