namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Args
{
    public class ModifyWorkFlowCompanyArg
    {
        public long CompanyId { get; set; }
        public long WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
