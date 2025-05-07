using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskPossibilities
{
    public class ModifyRiskPossibilityCommand : ICommand<Result<long>>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Possibility { get; set; }
    }
}
