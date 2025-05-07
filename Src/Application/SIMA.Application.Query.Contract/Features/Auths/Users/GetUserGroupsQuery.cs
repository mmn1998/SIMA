namespace SIMA.Application.Query.Contract.Features.Auths.Users
{
    public class GetUserGroupsQuery
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public string? GroupName { get; set; }
    }
}
