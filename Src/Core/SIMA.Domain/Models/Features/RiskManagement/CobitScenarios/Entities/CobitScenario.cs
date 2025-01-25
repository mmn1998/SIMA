using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarioCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;

public class CobitScenario : Entity
{
    private CobitScenario()
    {
        
    }
    private CobitScenario(CreateCobitScenarioArg arg)
    {
        
    }
    public CobitScenarioId Id { get; private set; }
    public CobitScenarioCategoryId CobitScenarioCategoryId { get; private set; }
    public virtual CobitScenarioCategory CobitScenarioCategory { get; private set; }
    public ScenarioId ScenarioId { get; private set; }
    public virtual Scenario Scenario { get; private set; }
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
