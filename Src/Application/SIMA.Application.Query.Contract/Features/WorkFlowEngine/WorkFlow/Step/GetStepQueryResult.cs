namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.Step;

public class GetStepQueryResult
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public long? WorkFlowId { get; set; }
    public string? WorkFlowName { get; set; }
    public long? ActionTypeId { get; set; }
    public long? MainEntityId { get; set; }
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public long? DomainId { get; set; }
    public string? DomainName { get; set; }
    public string? ActiveStatus { get; set; }
    public string? BpmnId { get; set; }
    public long FormId { get; set; }
    public string FormName { get; set; }
}
