namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserQueryForAggregate
{
    public long Id { get; set; }
    public long? ProfileId { get; set; }
    public long? CompanyId { get; set; }
    public string Username { get; set; }
}
