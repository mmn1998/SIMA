namespace SIMA.Application.Query.Contract.Features.Auths.Roles
{
    public class GetRoleUserQuery
    {
        public long userId { get; set; }
        public string? UserName { get; set; }
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public long ProfileId { get; set; }
        public string? FullName { get; set; }
        public string? NationalID { get; set; }

    }
}
