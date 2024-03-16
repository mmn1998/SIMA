using SIMA.Domain.Models.Features.Auths.AddressTypes.Args;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;

public class AddressType : Entity
{
    private AddressType()
    {

    }

    private AddressType(CreateAddressTypeArg arg)
    {
        Id = new AddressTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<AddressType> Create(CreateAddressTypeArg arg, IAddressTypeDomainService service)
    {
        await CreateGuards(arg, service);
        return new AddressType(arg);
    }


    public async Task Modify(ModifyAddressTypeArg arg, IAddressTypeDomainService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }


    public AddressTypeId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }

    private List<AddressBook> _addressBooks = new();

    public ICollection<AddressBook> AddressBooks => _addressBooks;
    #region Gaurds

    private static async Task CreateGuards(CreateAddressTypeArg arg, IAddressTypeDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyAddressTypeArg arg, IAddressTypeDomainService service)
    {
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();
        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
}
