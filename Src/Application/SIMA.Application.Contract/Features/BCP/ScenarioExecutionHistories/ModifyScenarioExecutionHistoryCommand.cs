using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.BCP.ScenarioExecutionHistories;

public class ModifyScenarioExecutionHistoryCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public long ScenarioId { get; set; }
    public string ExecutionDate { get; set; }
    public long ExecutionNumber { get; set; }
    public string? ExecutionTimeFrom { get; set; }
    public string? ExecutionTimeTo { get; set; }
    public string? Description { get; set; }
}
