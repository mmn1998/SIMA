namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskDegrees
{
    public class GetRiskDegreesQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Color { get; set; }
        public float Degree { get; set; }
        public string IsImportantBia { get; set; }
        public string ActiveStatus { get; set; }
    }
}
