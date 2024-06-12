using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures
{
    public class GetAllRiskLevelMeasuresQueryResult
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
        public string RiskLevelName { get; set; }
        public string RiskLevelCode { get; set; }
        public float Level { get; set; }
        public string ImpactName { get; set; }
        public string ImpactCode { get; set; }
        public float Impact { get; set; }
        public string PossibilityName { get; set; }
        public string PossibilityCode { get; set; }
        public float Possibility { get; set; }

    }
}
