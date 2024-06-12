using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.RiskManagers.ThreatTypes
{
    public class DeleteThreatTypeCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
    }
}
