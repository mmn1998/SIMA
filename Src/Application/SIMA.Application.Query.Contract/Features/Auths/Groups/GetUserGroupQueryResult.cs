namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetUserGroupQueryResult
{
    public long Id { get; set; }
    public long GroupId { get; set; }
    public long UserId { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
}
