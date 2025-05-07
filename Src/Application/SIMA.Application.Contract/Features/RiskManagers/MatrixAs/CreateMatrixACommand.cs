using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.MatrixAs;

public class CreateMatrixACommand : ICommand<Result<long>>
{
    public string Code { get; set; }
    public long TriggerStatusId { get; set; }
    public long MatrixAValueId { get; set; }
    public long UseVulnerabilityId { get; set; }
}