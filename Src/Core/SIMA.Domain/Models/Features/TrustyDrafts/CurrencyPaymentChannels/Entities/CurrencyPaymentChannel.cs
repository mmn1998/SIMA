using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Contracts;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.ValueObjects;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;

public class CurrencyPaymentChannel : Entity, IAggregateRoot
{
    private CurrencyPaymentChannel()
    {

    }
    private CurrencyPaymentChannel(CreateCurrencyPaymentChannelArg arg)
    {
        Id = new(arg.Id);
        LocationId = new(arg.LocationId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<CurrencyPaymentChannel> Create(CreateCurrencyPaymentChannelArg arg, ICurrencyPaymentChannelDomainService service)
    {
        await CreateGuards(arg, service);
        return new CurrencyPaymentChannel(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateCurrencyPaymentChannelArg arg, ICurrencyPaymentChannelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyCurrencyPaymentChannelArg arg, ICurrencyPaymentChannelDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public async Task Modify(ModifyCurrencyPaymentChannelArg arg, ICurrencyPaymentChannelDomainService service)
    {
        await ModifyGuards(arg, service);
        LocationId = new(arg.LocationId);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public CurrencyPaymentChannelId Id { get; private set; }
    public LocationId? LocationId { get; private set; }
    public virtual Location? Location { get; private set; }
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

    private List<WageRate> _wageRates = new();
    public ICollection<WageRate> WageRates => _wageRates;
}