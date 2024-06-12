using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ImpactScales
{
    public class DeleteImpactScaleCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
