using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskDegrees
{
    public class DeleteRiskDegreeCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
