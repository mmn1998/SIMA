namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskImpacts
{
    public class GetRiskImpactsQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Impact { get; set; }
        public string ActiveStatus { get; set; }
    }
}
