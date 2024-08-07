namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;

public class NextProgressDetails
{
    public long ActionTypeId { get; set; }
    public long ProgressId { get; set; }
    public long TargetId { get; set; }
}
