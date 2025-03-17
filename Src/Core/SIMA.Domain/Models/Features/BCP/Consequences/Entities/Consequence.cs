using SIMA.Domain.Models.Features.BCP.AnalysisValues.Entities;
using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.Args;
using SIMA.Domain.Models.Features.BCP.Consequences.Contracts;
using SIMA.Domain.Models.Features.BCP.Consequences.ValueObjects;
using SIMA.Domain.Models.Features.BCP.ConsequenceValues.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.Consequences.Entities;

public class Consequence : Entity, IAggregateRoot
{
    private Consequence()
    {

    }
    private Consequence(CreateConsequenceArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<Consequence> Create(CreateConsequenceArg arg, IConsequenceDomainService service)
    {
        await CreateGuards(arg, service);
        return new Consequence(arg);
    }
    public async Task Modify(ModifyConsequenceArg arg, IConsequenceDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    #region Guards
    private static async Task CreateGuards(CreateConsequenceArg arg, IConsequenceDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyConsequenceArg arg, IConsequenceDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ConsequenceId Id { get; set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long ActiveStatusId { get; private set; }
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
    private List<BusinessImpactAnalysisDisasterOrigin> _businessImpactAnalysisDisasterOrigins = new();
    public ICollection<BusinessImpactAnalysisDisasterOrigin> BusinessImpactAnalysisDisasterOrigins => _businessImpactAnalysisDisasterOrigins;

    private List<ConsequenceValue> _consequenceValues = new();
    public ICollection<ConsequenceValue>  ConsequenceValues => _consequenceValues;

    private List<BiaValue> _biaValues = new();
    public ICollection<BiaValue>  BiaValues => _biaValues;

    private List<AnalysisValue> _analysisValues = new();
    public ICollection<AnalysisValue> AnalysisValues => _analysisValues;
}
