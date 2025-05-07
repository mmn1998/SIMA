namespace SIMA.Application.Query.Contract.Features.Auths.OwnershipTypes;

public class GetOwnershipTypeQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}

