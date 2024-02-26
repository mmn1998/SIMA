using SIMA.Domain.Models.Features.Auths.PhoneTypes.Args;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;

public class PhoneType : Entity
{
    private PhoneType(CreatePhoneTypeArg arg)
    {
        Id = new PhoneTypeId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedBy = arg.CreatedBy;
        CreatedAt = arg.CreatedAt;
    }
    private PhoneType() { }
    public static async Task<PhoneType> Create(CreatePhoneTypeArg arg, IPhoneTypeService service)
    {
        await CreateGuards(arg, service);
        return new PhoneType(arg);
    }


    public async void Modify(ModifyPhoneTypeArg arg, IPhoneTypeService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }

    #region Guards
    private static async Task CreateGuards(CreatePhoneTypeArg arg, IPhoneTypeService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyPhoneTypeArg arg, IPhoneTypeService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion
    public PhoneTypeId Id { get; private set; }

    public string? Name { get; private set; }
    public long ActiveStatusId { get; private set; }

    public string? Code { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }
    private List<PhoneBook> _phoneBooks = new();
    public virtual ICollection<PhoneBook> PhoneBooks => _phoneBooks;
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
