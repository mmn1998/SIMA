using SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels;
using SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities;

namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevelMeasures
{
    public class GetRiskLevelMeasuresQueryResult
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
        public GetRiskLevelsQueryResult RiskLevel { get; set; }
        public GetRiskImpactsQueryResult Impact { get; set; }
        public GetRiskPossibilitiesQueryResult Possibility { get; set; }
    }
    
}
