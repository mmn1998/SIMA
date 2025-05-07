namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskPossibilities
{
    public class GetRiskPossibilitiesQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Possibility { get; set; }
        public string ActiveStatus { get; set; }
    }
}
