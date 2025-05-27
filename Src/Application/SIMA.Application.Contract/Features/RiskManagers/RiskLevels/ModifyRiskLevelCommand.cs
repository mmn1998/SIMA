using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskLevels
{
    public class ModifyRiskLevelCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public long RiskValueId { get; set; }
        public long SeverityValueId { get; set; }
        public long CurrentOccurrenceProbabilityValueId { get; set; }
        public string Code { get; set; }

    }
}
