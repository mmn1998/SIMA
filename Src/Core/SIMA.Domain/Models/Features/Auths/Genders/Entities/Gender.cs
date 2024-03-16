using SIMA.Domain.Models.Features.Auths.Genders.Args;
using SIMA.Domain.Models.Features.Auths.Genders.Interfaces;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Genders.Entities;

public class Gender : Entity
{
    private Gender(CreateGenderArg arg)
    {
        Id = new GenderId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId.Value;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    private Gender() 
    {
    }
    public static async Task<Gender> Create(CreateGenderArg arg, IGenderService service)
    {
        await CreateGuards(arg, service);
        return new Gender(arg);
    }

    public async Task Modify(ModifyGenderArg arg, IGenderService service)
    {
        
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;


    }
    #region Guards
    private static async Task CreateGuards(CreateGenderArg arg, IGenderService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw SimaResultException.UniqueCodeError;
    }
    private async Task ModifyGuards(ModifyGenderArg arg, IGenderService service)
    {
        arg.NullCheck();
        arg.Code.NullCheck();
        arg.Name.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw SimaResultException.UniqueCodeError;
    }
    #endregion

    public GenderId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    private List<Profile> _profiles = new();
    public ICollection<Profile> Profiles => _profiles;
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
