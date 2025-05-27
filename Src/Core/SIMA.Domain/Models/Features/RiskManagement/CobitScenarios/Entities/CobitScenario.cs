using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Args;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;

public class CobitScenario : Entity
{
    private CobitScenario()
    {
        
    }
    private CobitScenario(CreateCobitScenarioArg arg)
    {
        Id = new(arg.Id);
        CobitRiskCategoryId = new(arg.CobitRiskCategoryId);
        ActiveStatusId = arg.ActiveStatusId;
        Name = arg.Name;
        Description = arg.Description;
        CobitIdentifier = arg.CobitIdentifier;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static CobitScenario Create(CreateCobitScenarioArg arg, ICobitScenarioDomainService service)
    {
        CreateGuards(arg, service);
        return new CobitScenario(arg);
    }
    public void Modify(ModifyCobitScenarioArg arg, ICobitScenarioDomainService service)
    {
        ModifyGuards(arg, service);
        CobitRiskCategoryId = new(arg.CobitRiskCategoryId);
        Name = arg.Name;
        CobitIdentifier = arg.CobitIdentifier;
        Description = arg.Description;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static void CreateGuards(CreateCobitScenarioArg arg, ICobitScenarioDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        if (arg.Name.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);

    }
    private void ModifyGuards(ModifyCobitScenarioArg arg, ICobitScenarioDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        if (arg.Name.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
    }
    #endregion
    public CobitScenarioId Id { get; private set; }
    public CobitCategoryId CobitRiskCategoryId { get; private set; }
    public virtual CobitCategory CobitRiskCategory { get; private set; }
    public long ActiveStatusId { get; private set; }
    public string Name { get; private set; }
    public string? CobitIdentifier { get; private set; }
    public string? Description { get; private set; }
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
    private List<BusinessContinuityPlanScenarioCobitScenario> _businessContinuityPlanScenarioCobitScenarios = new();
    public ICollection<BusinessContinuityPlanScenarioCobitScenario> BusinessContinuityPlanScenarioCobitScenarios => _businessContinuityPlanScenarioCobitScenarios;
    private List<CobitRiskCategoryScenario> _cobitRiskCategoryScenarios = new();
    public ICollection<CobitRiskCategoryScenario> CobitRiskCategoryScenarios => _cobitRiskCategoryScenarios;
}
