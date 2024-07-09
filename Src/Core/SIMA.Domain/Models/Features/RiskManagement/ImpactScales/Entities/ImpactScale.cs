using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Args;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;

public class ImpactScale : Entity
{
    private ImpactScale()
    {
        
    }
    private ImpactScale(CreateImpactScaleArg arg)
    {
        Id = new ImpactScaleId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ImpactScale> Create(CreateImpactScaleArg arg ,  IImpactScaleDomainService service)
    {
        await CreateGuard(arg, service);
        return new ImpactScale(arg);
    }
    public async Task Modify(ModifyImpactScaleArg arg, IImpactScaleDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
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
    public ImpactScaleId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceRiskImpact> _serviceRiskImpacts = new();
    public ICollection<ServiceRiskImpact> ServiceRiskImpacts => _serviceRiskImpacts;


    #region Guards
    private static async Task CreateGuard(CreateImpactScaleArg arg, IImpactScaleDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyImpactScaleArg arg, IImpactScaleDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
}
