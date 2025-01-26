using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ConsequenceLevels;

public class DeleteConsequenceLevelCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
}