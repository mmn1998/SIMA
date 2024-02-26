namespace SIMA.Application.Query.Contract.Features.Auths.Users;

public class GetUserDomainQueryForAggregate
{
    public long UserDomainId { get; set; }
    public long DomainId { get; set; }
    public string DomainName { get; set; }
}
