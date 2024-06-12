using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevels
{
    public class DeleteRiskLevelCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
