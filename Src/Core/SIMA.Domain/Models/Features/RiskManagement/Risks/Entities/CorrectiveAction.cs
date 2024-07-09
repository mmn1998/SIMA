using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

public class CorrectiveAction : Entity
{
    private CorrectiveAction()
    {
        
    }
    private CorrectiveAction(CreateCorrectiveActionArg arg)
    {
        Id = new CorrectiveActionId(IdHelper.GenerateUniqueId());
        RiskId = new RiskId(arg.RiskId);
        ActionDescription = arg.ActionDescription;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static  CorrectiveAction Create(CreateCorrectiveActionArg arg)
    {
        return new CorrectiveAction(arg);
    }
    public async Task Modify(ModifyCorrectiveActionArg arg)
    {
        RiskId = new RiskId(arg.RiskId);
        ActionDescription = arg.ActionDescription;
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

    public CorrectiveActionId Id { get; private set; }
    public RiskId RiskId { get; private set; }
    public virtual Risk Risk { get; private set; }
    public string ActionDescription { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
}
