namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public class RequestRoleManagement_GetRolesDto
    {
        public string? SearchKey { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
