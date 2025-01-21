using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;

public class EvaluationCriteria : Entity, IAggregateRoot
{
    private EvaluationCriteria()
    {
    }
    private EvaluationCriteria(CreateEvaluationCriteriaArg arg)
    {
        Id = new(arg.Id);
        RiskDegreeId = new(arg.RiskDegreeId);
        RiskImpactId = new(arg.RiskImpactId);
        RiskPossibilityId = new(arg.riskPossibilityId);
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<EvaluationCriteria> Create(CreateEvaluationCriteriaArg arg, IEvaluationCriteriaDomainService service)
    {
        await CreateGuards(arg, service);
        return new EvaluationCriteria(arg);
    }
    public async Task Modify(ModifyEvaluationCriteriaArg arg, IEvaluationCriteriaDomainService service)
    {
        await ModifyGuards(arg, service);
        RiskDegreeId = new(arg.RiskDegreeId);
        RiskImpactId = new(arg.RiskImpactId);
        RiskPossibilityId = new(arg.RiskPossibilityId);
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateEvaluationCriteriaArg arg, IEvaluationCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsUnique(new(arg.riskPossibilityId), new(arg.RiskImpactId), new(arg.RiskDegreeId))) throw new SimaResultException(CodeMessges._100111Code, Messages.RiskPossibilityIdRiskImpactIdRiskDegreeIdUniqueError);
    }
    private async Task ModifyGuards(ModifyEvaluationCriteriaArg arg, IEvaluationCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
        if (!await service.IsUnique(new(arg.RiskPossibilityId), new(arg.RiskImpactId), new(arg.RiskDegreeId), Id)) throw new SimaResultException(CodeMessges._100111Code, Messages.RiskPossibilityIdRiskImpactIdRiskDegreeIdUniqueError);
    }
    #endregion
    public EvaluationCriteriaId Id { get; private set; }
    public RiskDegreeId RiskDegreeId { get; private set; }
    public virtual RiskDegree RiskDegree { get; private set; }
    public RiskPossibilityId RiskPossibilityId { get; private set; }
    public virtual RiskPossibility RiskPossibility { get; private set; }
    public RiskImpactId RiskImpactId { get; private set; }
    public virtual RiskImpact RiskImpact { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    public void Delete(long userId)
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    }
    public void Active(long userId)
    {
        ActiveStatusId = (long)ActiveStatusEnum.Active;
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    }
}
