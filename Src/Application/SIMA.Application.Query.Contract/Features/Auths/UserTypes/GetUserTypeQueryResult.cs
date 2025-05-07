namespace SIMA.Application.Query.Contract.Features.Auths.UserTypes;

public class GetUserTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}