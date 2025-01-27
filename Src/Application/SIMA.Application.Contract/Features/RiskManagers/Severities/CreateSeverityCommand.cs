using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Severities;

public class CreateSeverityCommand : ICommand<Result<long>>
{
    public string Code { get; set; }
    public long ConsequenceCategoryId { get; set; }
    public long SeverityValueId { get; set; }
    public long AffectedHistoryId { get; set; }
}