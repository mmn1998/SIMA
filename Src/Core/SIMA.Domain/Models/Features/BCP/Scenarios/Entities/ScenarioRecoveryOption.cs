using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class ScenarioRecoveryOption : Entity
{
    private ScenarioRecoveryOption()
    {

    }
    private ScenarioRecoveryOption(CreateScenarioRecoveryOptionArg arg)
    {
        Id = new(arg.Id);
        RecoveryOptionPriorityId = new(arg.RecoveryOptionPriorityId);
        ScenarioId = new(arg.ScenarioId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static ScenarioRecoveryOption Create(CreateScenarioRecoveryOptionArg arg)
    {
        return new ScenarioRecoveryOption(arg);
    }
    public void Modify(ModifyScenarioRecoveryOptionArg arg)
    {
        RecoveryOptionPriorityId = new(arg.RecoveryOptionPriorityId);
        ScenarioId = new(arg.ScenarioId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    public ScenarioRecoveryOptionId Id { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
    public RecoveryOptionPriorityId RecoveryOptionPriorityId { get; private set; }
    public virtual RecoveryOptionPriority RecoveryOptionPriority { get; private set; }
    public string? Description { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
