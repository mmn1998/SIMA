using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Args;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.ValueObjects;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;

public class ConsequenceCategory : Entity, IAggregateRoot
{
    private ConsequenceCategory()
    {
        
    }
    private ConsequenceCategory(CreateConsequenceCategoryArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<ConsequenceCategory> Create(CreateConsequenceCategoryArg arg, IConsequenceCategoryDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new ConsequenceCategory(arg);
    }
    public async Task Modify(ModifyConsequenceCategoryArg arg, IConsequenceCategoryDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }    
    public ConsequenceCategoryId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateConsequenceCategoryArg arg, IConsequenceCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifyConsequenceCategoryArg arg, IConsequenceCategoryDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }    
    private List<ConsequenceLevelCategory> _riskConsequences = new();
    public ICollection<ConsequenceLevelCategory> RiskConsequences => _riskConsequences;
    private List<Risk> _risks = new();
    public ICollection<Risk> Risks => _risks;
}