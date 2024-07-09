using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Args;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Contracts;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;

public class HappeningPossibility : Entity, IAggregateRoot
{
    private HappeningPossibility()
    {

    }
    private HappeningPossibility(CreateHappeningPossibilityArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        Ordering = arg.Ordering;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<HappeningPossibility> Create(CreateHappeningPossibilityArg arg, IHappeningPossibilityDomainService service)
    {
        await CreateGuards(arg, service);
        return new HappeningPossibility(arg);
    }
    public async Task Modify(ModifyHappeningPossibilityArg arg, IHappeningPossibilityDomainService service)
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
    private static async Task CreateGuards(CreateHappeningPossibilityArg arg, IHappeningPossibilityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyHappeningPossibilityArg arg, IHappeningPossibilityDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public HappeningPossibilityId Id { get; set; }
    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public float Ordering { get; private set; }
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
}
