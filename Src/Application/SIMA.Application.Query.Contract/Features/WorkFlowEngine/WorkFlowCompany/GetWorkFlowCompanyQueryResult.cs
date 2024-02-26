namespace SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlowCompany
{
    public class GetWorkFlowCompanyQueryResult
    {
        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public long? ActiveStatusId { get; set; }
    }
}
