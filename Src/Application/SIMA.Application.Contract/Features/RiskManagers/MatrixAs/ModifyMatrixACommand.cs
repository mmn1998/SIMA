using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.MatrixAs;

public class ModifyMatrixACommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long TriggerStatusId { get; set; }
    public long MatrixAValueId { get; set; }
    public long UseVulnerabilityId { get; set; }
}