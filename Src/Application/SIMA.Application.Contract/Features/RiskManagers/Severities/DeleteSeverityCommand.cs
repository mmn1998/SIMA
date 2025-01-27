using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.Severities;

public class DeleteSeverityCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}