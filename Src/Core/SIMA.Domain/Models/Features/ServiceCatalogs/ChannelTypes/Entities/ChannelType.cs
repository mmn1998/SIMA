using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Args;
using SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Interfaces;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;

namespace SIMA.Domain.Models.Features.ServiceCatalogs.ChannelTypes.Entities;

public class ChannelType : Entity
{
    private ChannelType()
    {
    }
    private ChannelType(CreateChannelTypeArg arg)
    {
        Id = new ChannelTypeId(arg.Id);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<ChannelType> Create(CreateChannelTypeArg arg, IChannelTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new ChannelType(arg);
    }
    public async Task Modify(ModifyChannelTypeArg arg, IChannelTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    #region Guards
    private static async Task CreateGuards(CreateChannelTypeArg arg, IChannelTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyChannelTypeArg arg, IChannelTypeDomainService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (arg.Name.Length > 200) throw new SimaResultException(CodeMessges._400Code, Messages.LengthNameException);
        if (arg.Code.Length > 20) throw new SimaResultException(CodeMessges._400Code, Messages.LengthCodeException);
        if (!await service.IsCodeUnique(arg.Code, Id)) throw new SimaResultException(CodeMessges._400Code, Messages.UniqueCodeError);
    }
    #endregion
    public ChannelTypeId Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public long? ActiveStatusId { get; private set; }
    public DateTime? CreatedAt { get; private set; }
    public long? CreatedBy { get; private set; }
    public byte[]? ModifiedAt { get; private set; }
    public long? ModifiedBy { get; private set; }

    private List<ServiceChannel> _serviceChanneles = new();
    public ICollection<ServiceChannel> ServiceChanneles => _serviceChanneles;
}
