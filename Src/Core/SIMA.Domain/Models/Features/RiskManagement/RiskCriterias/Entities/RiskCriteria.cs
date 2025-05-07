using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;

public class RiskCriteria : Entity
{
    private RiskCriteria()
    {
        
    }
    private RiskCriteria(CreateRiskCriteriaArg arg)
    {
        Id = new RiskCriteriaId(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        RiskDegreeId = new RiskDegreeId(arg.RiskDegreeId);
        RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
        RiskImpactId = new RiskImpactId(arg.RiskImpactId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<RiskCriteria> Create(CreateRiskCriteriaArg arg, IRiskCriteriaDomainService service)
    {
        await CreateGuard(arg, service);
        return new RiskCriteria(arg);
    }
    public async Task Modify(ModifyRiskCriteriaArg arg, IRiskCriteriaDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        RiskDegreeId = new RiskDegreeId(arg.RiskDegreeId);
        RiskPossibilityId = new RiskPossibilityId(arg.RiskPossibilityId);
        RiskImpactId = new RiskImpactId(arg.RiskImpactId);
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
    public RiskCriteriaId Id { get; set; }
    public string Code { get; private set; }
    public RiskDegreeId RiskDegreeId { get; private set; }
    public virtual RiskDegree RiskDegree { get; private set; }
    public RiskPossibilityId RiskPossibilityId { get; private set; }
    public virtual RiskPossibility RiskPossibility { get; private set; }
    public RiskImpactId RiskImpactId { get; private set; }
    public virtual RiskImpact RiskImpact { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    #region Guards
    private static async Task CreateGuard(CreateRiskCriteriaArg arg, IRiskCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();

        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyRiskCriteriaArg arg, IRiskCriteriaDomainService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();

        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion

}
