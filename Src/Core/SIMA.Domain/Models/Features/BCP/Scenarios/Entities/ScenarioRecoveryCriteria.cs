using SIMA.Domain.Models.Features.BCP.Scenarios.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Scenarios.Entities;

public class ScenarioRecoveryCriteria : Entity
{
    private ScenarioRecoveryCriteria()
    {

    }
    private ScenarioRecoveryCriteria(CreateScenarioRecoveryCriteriaArg arg)
    {
        Id = new(arg.Id);
        ScenarioId = new(arg.ScenarioId);
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ScenarioRecoveryCriteria> Create(CreateScenarioRecoveryCriteriaArg arg)
    {
        return new ScenarioRecoveryCriteria(arg);
    }
    public ScenarioRecoveryCriteriaId Id { get; set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
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
