using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskTypes
{
    public class DeleteRiskTypeCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
