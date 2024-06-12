namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskTypes
{
    public class GetRiskTypesQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
    }
}
