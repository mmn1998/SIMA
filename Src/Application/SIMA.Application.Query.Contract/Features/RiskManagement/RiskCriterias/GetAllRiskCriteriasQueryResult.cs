namespace SIMA.Application.Query.Contract.Features.RiskManagement.RiskCriterias
{
    public class GetAllRiskCriteriasQueryResult
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string ActiveStatus { get; set; }
        public string DegreeName { get; set; }
        public string DegreeCode { get; set; }
        public string ImpactName { get; set; }
        public string ImpactCode { get; set; }
        public float Impact { get; set; }
        public string PossibilityName { get; set; }
        public string PossibilityCode { get; set; }
        public float Possibility { get; set; }
    }
}
