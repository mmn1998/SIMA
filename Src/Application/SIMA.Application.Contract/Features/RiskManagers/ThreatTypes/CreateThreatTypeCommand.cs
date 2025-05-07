using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.ThreatTypes
{
    public class CreateThreatTypeCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
