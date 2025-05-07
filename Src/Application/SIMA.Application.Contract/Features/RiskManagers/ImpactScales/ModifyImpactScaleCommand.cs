using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ImpactScales
{
    public class ModifyImpactScaleCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
