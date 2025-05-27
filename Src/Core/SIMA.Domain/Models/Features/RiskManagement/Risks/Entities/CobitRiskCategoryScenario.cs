using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;

public class CobitRiskCategoryScenario
{
    private CobitRiskCategoryScenario()
    {

    }
    private CobitRiskCategoryScenario(CreateCobitRiskCategoryScenarioArg arg)
    {
        Id = new(arg.Id);
        RiskId = new(arg.RiskId);
        CobitCategoryId = new(arg.CobitCategoryId);
        CobitScenarioId = new (arg.CobitScenarioId);
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static CobitRiskCategoryScenario Create(CreateCobitRiskCategoryScenarioArg arg)
    {
        return new CobitRiskCategoryScenario(arg);
    }
    public CobitRiskCategoryScenarioId Id { get; private set; }
    public CobitScenarioId CobitScenarioId { get; private set; }
    public virtual CobitScenario CobitScenario { get; private set; }
    public RiskId RiskId { get; private set; }
    public virtual Risk Risk { get; private set; }
    public CobitCategoryId CobitCategoryId { get; private set; }
    public virtual CobitCategory CobitCategory { get; private set; }
    public long ActiveStatusId { get; private set; }
    public string Code { get; private set; }
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
