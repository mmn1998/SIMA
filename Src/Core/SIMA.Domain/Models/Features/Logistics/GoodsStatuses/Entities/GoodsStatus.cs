using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Args;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Contracts;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.ValueObjects;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
public class GoodsStatus : Entity
{
    private GoodsStatus()
    {
    }
    public GoodsStatus(CreateGoodsStatusArg arg)
    {
        Id = new(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        IsRequiredItConfirmation = arg.IsRequiredItConfirmation;
        IsFinal = arg.IsFinal;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<GoodsStatus> Create(CreateGoodsStatusArg arg, IGoodsStatusDomainService service)
    {
        await CreateGuards(arg, service);
        return new GoodsStatus(arg);
    }
    public async Task Modify(ModifyGoodsStatusArg arg, IGoodsStatusDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        IsFinal = arg.IsFinal;
        IsRequiredItConfirmation = arg.IsRequiredItConfirmation;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    #region Guards
    private static async Task CreateGuards(CreateGoodsStatusArg arg, IGoodsStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyGoodsStatusArg arg, IGoodsStatusDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion

    public GoodsStatusId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string IsRequiredItConfirmation { get; private set; }
    public string? IsFinal { get; private set; }
    public long ActiveStatusId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public long CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<LogisticsRequestGoods> _logisticsRequestGoods = new();
    public ICollection<LogisticsRequestGoods> LogisticsRequestGoods => _logisticsRequestGoods;
}
