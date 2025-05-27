namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskLevels
{
    public class GetRiskLevelsQueryResult
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long RiskValueId { get; set; }
        public string? RiskValueName { get; set; }
        public long SeverityValueId { get; set; }
        public string? SeverityValueName { get; set; }
        public long CurrentOccurrenceProbabilityValueId { get; set; }
        public string? CurrentOccurrenceProbabilityValueName { get; set; }
        public string ActiveStatus { get; set; }
    }
}
