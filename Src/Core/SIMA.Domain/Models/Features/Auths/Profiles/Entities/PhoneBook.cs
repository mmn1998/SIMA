using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Entities;

public class PhoneBook
{
    private PhoneBook()
    {

    }
    private PhoneBook(CreatePhoneBookArg arg)
    {
        Id = new PhoneBookId(IdHelper.GenerateUniqueId());
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PhoneTypeId.HasValue) PhoneTypeId = new PhoneTypeId(arg.PhoneTypeId.Value);
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<PhoneBook> Create(CreatePhoneBookArg arg)
    {
        return new PhoneBook(arg);
    }
    public async Task Modify(ModifyPhoneBookArg arg)
    {
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.PhoneTypeId.HasValue) PhoneTypeId = new PhoneTypeId(arg.PhoneTypeId.Value);
        PhoneNumber = arg.PhoneNumber;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }

    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }

    public PhoneBookId Id { get; private set; }

    public ProfileId? ProfileId { get; private set; }

    public PhoneTypeId? PhoneTypeId { get; private set; }

    public string? PhoneNumber { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual PhoneType? PhoneType { get; private set; }

    public virtual Profile? Profile { get; private set; }
}
