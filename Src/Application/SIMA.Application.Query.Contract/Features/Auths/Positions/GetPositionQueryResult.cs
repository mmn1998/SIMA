namespace SIMA.Application.Query.Contract.Features.Auths.Positions;

public class GetPositionQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? DepartemantName { get; set; }
    public string? CompanyName { get; set; }
    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }
    public bool IsDeactivated { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}
