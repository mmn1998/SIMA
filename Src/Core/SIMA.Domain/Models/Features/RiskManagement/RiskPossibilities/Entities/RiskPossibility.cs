using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Interfaces;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;

public class RiskPossibility : Entity
{
    private RiskPossibility()
    {

    }
    private RiskPossibility(CreateRiskPossibilityArgs arg)
    {
        Id = new RiskPossibilityId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        Possibility = arg.Possibility;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<RiskPossibility> Create(CreateRiskPossibilityArgs arg, IRiskPossibilityDomainService service)
    {
        await CreateGuard(arg, service);
        return new RiskPossibility(arg);
    }
    public async Task Modify(ModifyRiskPossibilityArgs arg, IRiskPossibilityDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Possibility = arg.Possibility;
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
    public RiskPossibilityId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public float Possibility { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<RiskCriteria> _riskCriterias = new();
    public ICollection<RiskCriteria> RiskCriterias => _riskCriterias;

    private List<RiskLevelMeasure> _riskLevelMeasures = new();
    public ICollection<RiskLevelMeasure> RiskLevelMeasures => _riskLevelMeasures;

    private List<Threat> _threats = new();
    public ICollection<Threat> Threats => _threats;

    #region Guards
    private static async Task CreateGuard(CreateRiskPossibilityArgs arg, IRiskPossibilityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }

    private async Task ModifyGuard(ModifyRiskPossibilityArgs arg, IRiskPossibilityDomainService service)
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
