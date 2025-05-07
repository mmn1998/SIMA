using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Args;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;

public class ScenarioExecutionHistory : Entity
{
    private ScenarioExecutionHistory()
    {

    }
    private ScenarioExecutionHistory(CreateScenarioExecutionHistoryArg arg)
    {
        Id = new(arg.Id);
        ScenarioId = new(arg.ScenarioId);
        ExecutionDate = arg.ExecutionDate;
        ExecutionNumber = arg.ExecutionNumber;
        ExecutionTimeFrom = arg.ExecutionTimeFrom;
        ExecutionTimeTo = arg.ExecutionTimeTo;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public async Task Modify(ModifyScenarioExecutionHistoryArg arg)
    {
        ScenarioId = new(arg.ScenarioId);
        ExecutionDate = arg.ExecutionDate;
        ExecutionNumber = arg.ExecutionNumber;
        ExecutionTimeFrom = arg.ExecutionTimeFrom;
        ExecutionTimeTo = arg.ExecutionTimeTo;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }


    public static ScenarioExecutionHistory Create(CreateScenarioExecutionHistoryArg arg)
    {
        return new ScenarioExecutionHistory(arg);
    }
    public ScenarioExecutionHistoryId Id { get; private set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
    public DateTime ExecutionDate { get; private set; }
    public long ExecutionNumber { get; private set; }
    public TimeOnly? ExecutionTimeFrom { get; private set; }
    public TimeOnly? ExecutionTimeTo { get; private set; }
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
    public void Active(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Active;
    }
}
