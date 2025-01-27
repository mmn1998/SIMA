using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Severities;

public class ModifySeverityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string Code { get; set; }
    public long ConsequenceLevelId { get; set; }
    public long SeverityValueId { get; set; }
    public long AffectedHistoryId { get; set; }
}