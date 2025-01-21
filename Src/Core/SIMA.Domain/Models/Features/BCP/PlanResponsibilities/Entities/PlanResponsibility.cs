using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Args;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Contracts;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;

public class PlanResponsibility : Entity
{
    private PlanResponsibility()
    {

    }
    private PlanResponsibility(CreatePlanResponsibilityArg arg)
    {
        Id = new PlanResponsibilityId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        Ordering = arg.Ordering;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<PlanResponsibility> Create(CreatePlanResponsibilityArg arg, IPlanResponsibilityDomainService service)
    {
        await CreateGuards(arg, service);
        return new PlanResponsibility(arg);
    }
    public async Task Modify(ModifyPlanResponsibilityArg arg, IPlanResponsibilityDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreatePlanResponsibilityArg arg, IPlanResponsibilityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyPlanResponsibilityArg arg, IPlanResponsibilityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code,Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public PlanResponsibilityId Id { get; set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public float Ordering { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<BusinessContinuityStratgyResponsible> _businessContinuityStratgyResponsible = new();
    public ICollection<BusinessContinuityStratgyResponsible> BusinessContinuityStratgyResponsibles => _businessContinuityStratgyResponsible;

    private List<BusinessContinuityPlanResponsible> _businessContinuityPlanResponsible = new();
    public ICollection<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles => _businessContinuityPlanResponsible;
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
