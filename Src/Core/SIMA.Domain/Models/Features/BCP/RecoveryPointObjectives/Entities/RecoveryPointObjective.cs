using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Args;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Contracts;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;

public class RecoveryPointObjective : Entity, IAggregateRoot
{
    private RecoveryPointObjective()
    {

    }
    private RecoveryPointObjective(CreateRecoveryPointObjectiveArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        RpoFrom = arg.RpoFrom;
        RpoTo = arg.RpoTo;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<RecoveryPointObjective> Create(CreateRecoveryPointObjectiveArg arg, IRecoveryPointObjectiveDomainService service)
    {
        await CreateGuards(arg, service);
        return new RecoveryPointObjective(arg);
    }
    public async Task Modify(ModifyRecoveryPointObjectiveArg arg, IRecoveryPointObjectiveDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        RpoFrom = arg.RpoFrom;
        RpoTo = arg.RpoTo;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateRecoveryPointObjectiveArg arg, IRecoveryPointObjectiveDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyRecoveryPointObjectiveArg arg, IRecoveryPointObjectiveDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public RecoveryPointObjectiveId Id { get; set; }
    public int RpoFrom { get; set; }
    public int RpoTo { get; set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    private List<BusinessImpactAnalysisDisasterOrigin> _businessImpactAnalysisDisasterOrigins = new();
    public ICollection<BusinessImpactAnalysisDisasterOrigin> BusinessImpactAnalysisDisasterOrigins => _businessImpactAnalysisDisasterOrigins;
}
