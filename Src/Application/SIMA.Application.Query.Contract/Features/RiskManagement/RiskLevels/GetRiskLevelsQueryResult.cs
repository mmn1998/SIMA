namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels
{
    public class GetRiskLevelsQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Level { get; set; }
        public string ActiveStatus { get; set; }
    }
}
