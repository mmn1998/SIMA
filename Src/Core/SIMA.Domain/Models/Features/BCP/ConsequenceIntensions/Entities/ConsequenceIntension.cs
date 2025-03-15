using SIMA.Domain.Models.Features.BCP.BiaValues.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Args;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Contracts;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;

public class ConsequenceIntension : Entity, IAggregateRoot
{
    private ConsequenceIntension()
    {

    }
    private ConsequenceIntension(CreateConsequenceIntensionArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ValueNumber = arg.ValueNumber;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<ConsequenceIntension> Create(CreateConsequenceIntensionArg arg, IConsequenceIntensionDomainService service)
    {
        await CreateGuards(arg, service);
        return new ConsequenceIntension(arg);
    }
    public async Task Modify(ModifyConsequenceIntensionArg arg, IConsequenceIntensionDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ValueNumber = arg.ValueNumber;
        ModifiedBy = arg.ModifiedBy;
        ModifiedAt = arg.ModifiedAt;
    }
    #region Guards
    private static async Task CreateGuards(CreateConsequenceIntensionArg arg, IConsequenceIntensionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyConsequenceIntensionArg arg, IConsequenceIntensionDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ConsequenceIntensionId Id { get; set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public float ValueNumber { get; private set; }
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

    private List<BiaValue> _biaValues = new();
    public ICollection<BiaValue> BiaValues => _biaValues;
}