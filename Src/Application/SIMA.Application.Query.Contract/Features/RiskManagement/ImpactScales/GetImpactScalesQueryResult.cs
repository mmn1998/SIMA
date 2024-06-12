namespace SIMA.Application.Query.Contract.Features.RiskManagement.ImpactScales
{
    public class GetImpactScalesQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
    }
}
