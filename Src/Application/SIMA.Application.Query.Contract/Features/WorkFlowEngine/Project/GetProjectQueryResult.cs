namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.Project;

public class GetProjectQueryResult
{
    public long Id { get; set; }
    public long? DomainId { get; set; }
    public string? Name { get; set; }
    public string? DomainName { get; set; }
    public string? Code { get; set; }
    public int? ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
}
