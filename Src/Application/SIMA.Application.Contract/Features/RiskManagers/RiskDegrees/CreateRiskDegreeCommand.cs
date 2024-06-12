using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.RiskManagers.RiskDegrees
{
    public class CreateRiskDegreeCommand : ICommand<Result<long>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public float Degree { get; set; }
        public string IsImportantBia { get; set; }
    }
}
