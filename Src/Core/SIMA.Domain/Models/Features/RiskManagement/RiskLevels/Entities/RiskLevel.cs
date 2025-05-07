using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;

public class RiskLevel : Entity
{
    private RiskLevel()
    {

    }
    private RiskLevel(CreateRiskLevelArgs arg)
    {
        Id = new RiskLevelId(IdHelper.GenerateUniqueId());
        Code = arg.Code;
        RiskValueId = new(arg.RiskValueId);
        SeverityValueId = new(arg.SeverityValueId);
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<RiskLevel> Create(CreateRiskLevelArgs arg, IRiskLevelDomainService service)
    {
        await CreateGuard(arg, service);
        return new RiskLevel(arg);
    }
    public async Task Modify(ModifyRiskLevelArgs arg, IRiskLevelDomainService service)
    {
        await ModifyGuard(arg, service);
        Code = arg.Code;
        RiskValueId = new(arg.RiskValueId);
        SeverityValueId = new(arg.SeverityValueId);
        CurrentOccurrenceProbabilityValueId = new(arg.CurrentOccurrenceProbabilityValueId);
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
    public RiskLevelId Id { get; private set; }

    public RiskValueId RiskValueId { get; private set; }
    public virtual RiskValue RiskValue { get; private set; }

    public SeverityValueId SeverityValueId { get; private set; }
    public virtual SeverityValue SeverityValue { get; private set; }

    public CurrentOccurrenceProbabilityValueId CurrentOccurrenceProbabilityValueId { get; private set; }
    public virtual CurrentOccurrenceProbabilityValue CurrentOccurrenceProbabilityValue { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<RiskLevelMeasure> _riskLevelMeasures = new();
    public ICollection<RiskLevelMeasure> RiskLevelMeasures => _riskLevelMeasures;

    private List<RiskLevelCobit> _riskLevelCobits = new();
    public ICollection<RiskLevelCobit> RiskLevelCobits => _riskLevelCobits;

    #region Guards
    private static async Task CreateGuard(CreateRiskLevelArgs arg, IRiskLevelDomainService service)
    {
        arg.NullCheck();
     
        arg.Code.NullCheck();

       
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyRiskLevelArgs arg, IRiskLevelDomainService service)
    {
        arg.NullCheck();
        
        arg.Code.NullCheck();

       
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion

}
