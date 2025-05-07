using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskPossibilities
{
    public class DeleteRiskPossibilityCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
