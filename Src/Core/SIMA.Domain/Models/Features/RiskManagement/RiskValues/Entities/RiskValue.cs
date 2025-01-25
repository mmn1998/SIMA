using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Args;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Contracts;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;

public class RiskValue : Entity, IAggregateRoot
{
    private RiskValue()
    {

    }
    private RiskValue(CreateRiskValueArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        Color = arg.Color;
        Condition = arg.Condition;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<RiskValue> Create(CreateRiskValueArg arg, IRiskValueDomainService service)
    {
        await CreateGuadrs(arg, service);
        return new RiskValue(arg);
    }
    public async Task Modify(ModifyRiskValueArg arg, IRiskValueDomainService service)
    {
        await ModifyGuard(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        Color = arg.Color;
        Condition = arg.Condition;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public RiskValueId Id { get; private set; }
    public string? Name { get; private set; }
    public string? Code { get; private set; }
    public string? Color { get; private set; }
    public string? Condition { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }
    #region Guards
    private static async Task CreateGuadrs(CreateRiskValueArg arg, IRiskValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.Color.NullCheck();
        arg.Condition.NullCheck();

        if (arg.Color.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuard(ModifyRiskValueArg arg, IRiskValueDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.Color.NullCheck();
        arg.Condition.NullCheck();

        if (arg.Color.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.ColorMaxLengthError);
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
}