using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow;

public class ModifyWorkFlowCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ProjectId { get; set; }
    public long? ManagerRoleId { get; set; }
    public string? Description { get; set; }
    public long? MainAggregateId { get; set; }
    public long ActiveStatusId { get; set; }
}
