using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities
{
    public class PreventiveAction : Entity
    {
        private PreventiveAction()
        {
            
        }
        private PreventiveAction(CreatePreventiveActionArg arg)
        {
            Id = new PreventiveActionId(IdHelper.GenerateUniqueId());
            RiskId = new RiskId(arg.RiskId);
            ActionDescription = arg.ActionDescription;
            ActiveStatusId = arg.ActiveStatusId;
            CreatedAt = arg.CreatedAt;
            CreatedBy = arg.CreatedBy;
        }

        public static PreventiveAction Create(CreatePreventiveActionArg arg)
        {
            return new PreventiveAction(arg);
        }
        public async Task Modify(ModifyPreventiveActionArg arg)
        {
            RiskId = new RiskId(arg.RiskId);
            ActionDescription = arg.ActionDescription;
            ModifiedAt = arg.ModifiedAt;
            ModifiedBy = arg.ModifiedBy;
            ActiveStatusId = arg.ActiveStatusId;
        }
        public void Delete()
        {
            ActiveStatusId = (int)ActiveStatusEnum.Delete;
        }
        public PreventiveActionId Id { get; private set; }
        public RiskId RiskId { get; private set; }
        public virtual Risk Risk { get; private set; }
        public string ActionDescription { get; private set; }
        public long ActiveStatusId { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public long? CreatedBy { get; private set; }
        public byte[]? ModifiedAt { get; private set; }
        public long? ModifiedBy { get; private set; }
    }
}
