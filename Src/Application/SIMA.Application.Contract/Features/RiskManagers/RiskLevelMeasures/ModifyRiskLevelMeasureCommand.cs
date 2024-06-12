using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevelMeasures
{
    public class ModifyRiskLevelMeasureCommand : ICommand<Result<long>>
    {
        public long  Id { get; set; }
        public string Code { get; set; }
        public long RiskLevelId { get; set; }
        public long RiskPossibilityId { get; set; }
        public long RiskImpactId { get; set; }
    }
}
