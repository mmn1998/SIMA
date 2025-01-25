using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Args;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;

public class ThreatType : Entity
{
    private ThreatType()
    {

    }
    private ThreatType(CreateThreatTypeArg arg)
    {
        Id = new ThreatTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ThreatType> Create(CreateThreatTypeArg arg, IThreatTypeDomainService service )
    {
        await CreateGuard( arg, service );
        return new ThreatType(arg);
    }
    public async Task Modify(ModifyThreatTypeArg arg ,IThreatTypeDomainService service)
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
    public ThreatTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<Threat> _threats = new();
    public ICollection<Threat> Threats => _threats;

    #region Guards
    private static async Task CreateGuard(CreateThreatTypeArg arg, IThreatTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyThreatTypeArg arg, IThreatTypeDomainService service)
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
