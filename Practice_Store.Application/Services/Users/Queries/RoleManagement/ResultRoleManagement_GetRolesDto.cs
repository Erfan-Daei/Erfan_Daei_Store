namespace Practice_Store.Application.Services.Users.Queries.RoleManagement
{
    public class ResultRoleManagement_GetRolesDto
    {
        public List<RoleManagement_RoleDto> Roles { get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }
    }
}
