using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities
{
    public class RiskRelatedIssue : Entity
    {
        private RiskRelatedIssue()
        {
                
        }
        private RiskRelatedIssue(CreateRiskRelatedIssueArg arg)
        {
            Id = new RiskRelatedIssueId(IdHelper.GenerateUniqueId());
            RiskId = new RiskId(arg.RiskId);
            IssueId = new IssueId(arg.IssueId);
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static RiskRelatedIssue Create(CreateRiskRelatedIssueArg arg)
        {
            return new RiskRelatedIssue(arg);
        }
        public async Task Modify(ModifyRiskRelatedIssueArg arg)
        {
            RiskId = new RiskId(arg.RiskId);
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete(long userId)
        {
            ModifiedBy = userId;
            ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            ActiveStatusId = (long)ActiveStatusEnum.Delete;
        }
        public RiskRelatedIssueId Id { get; private set; }
        public RiskId RiskId { get; private set; }
        public virtual Risk Risk { get; private set; }
        public IssueId IssueId { get; private set; }
        public virtual Issue Issue { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
