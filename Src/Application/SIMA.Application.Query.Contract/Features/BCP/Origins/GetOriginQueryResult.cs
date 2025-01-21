namespace SIMA.Application.Query.Contract.Features.BCP.Origins;

public class GetOriginQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ActiveStatus { get; set; }
}