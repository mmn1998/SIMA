using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevelMeasures
{
    public class DeleteRiskLevelMeasureCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
