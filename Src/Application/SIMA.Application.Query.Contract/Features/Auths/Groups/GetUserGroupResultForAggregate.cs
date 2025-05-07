namespace SIMA.Application.Query.Contract.Features.Auths.Groups;

public class GetUserGroupResultForAggregate
{
    public long? UserId { get; set; }
    public string? NationalCode { get; set; }
    public string? FullName { get; set; }
}
