namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Args
{
    public class CreateWorkFlowCompanyArg
    {
        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get;  set; }
        public DateTime? ActiveTo { get;  set; }
        public long? ActiveStatusId { get;  set; }
        public DateTime? CreatedAt { get;  set; }
        public long? CreatedBy { get;  set; }
       
    }
}
