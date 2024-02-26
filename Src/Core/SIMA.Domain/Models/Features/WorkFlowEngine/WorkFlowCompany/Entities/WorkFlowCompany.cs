using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.ValueObjects;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.ValueObject;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities
{
    public class WorkFlowCompany : Entity
    {
        private WorkFlowCompany()
        {

        }
        private WorkFlowCompany(CreateWorkFlowCompanyArg arg)
        {
            Id = new WorkFlowCompanyId(IdHelper.GenerateUniqueId());
            WorkFlowId = new WorkFlowId(arg.WorkFlowId);
            CompanyId = arg.CompanyId;
            ActiveFrom = arg.ActiveFrom;
            ActiveTo = arg.ActiveTo;    
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public static async Task<WorkFlowCompany> New(CreateWorkFlowCompanyArg arg)
        {
            return new WorkFlowCompany(arg);
        }
        public void Modify(ModifyWorkFlowCompanyArg arg)
        {
            WorkFlowId = new WorkFlowId(arg.WorkFlowId);
            CompanyId = arg.CompanyId;
            ActiveFrom = arg.ActiveFrom;
            ActiveTo = arg.ActiveTo;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
        }
        public void Deactive()
        {
            ActiveStatusId = (long)ActiveStatusEnum.Deactive;
        }
        public WorkFlowCompanyId Id { get; private set; }
        public long CompanyId { get; set; }
        public WorkFlowId WorkFlowId { get; set; }
        public DateTime? ActiveFrom { get; private set; }
        public DateTime? ActiveTo { get; private set; }
        public long? ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; private set; }
        public WorkFlow.Entities.WorkFlow WorkFlow { get; set; }
    }
}
