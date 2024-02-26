using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Entities;

public class AddressBook
{
    private AddressBook()
    {

    }
    private AddressBook(CreateAddressBookArg arg)
    {
        Id = new AddressBookId(IdHelper.GenerateUniqueId());
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value);
        if (arg.AddressTypeId.HasValue) AddressTypeId = new AddressTypeId(arg.AddressTypeId.Value);
        Address = arg.Address;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<AddressBook> Create(IProfileService service, CreateAddressBookArg arg)
    {
        /// TODO change to domainservice
        //var validator = new AddressBookValidator(service);
        //await validator.ValidateAndThrowAsync(arg);
        return new AddressBook(arg);
    }
    public void Modify(ModifyAddressBookArg arg)
    {
        if (arg.ProfileId.HasValue) ProfileId = new ProfileId(arg.ProfileId.Value);
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value);
        if (arg.AddressTypeId.HasValue) AddressTypeId = new AddressTypeId(arg.AddressTypeId.Value);
        Address = arg.Address;
        PostalCode = arg.PostalCode;
        ActiveStatusId = arg.ActiveStatusId;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
    }
    public AddressBookId Id { get; private set; }

    public ProfileId? ProfileId { get; private set; }

    public AddressTypeId? AddressTypeId { get; private set; }

    public LocationId? LocationId { get; private set; }

    public string? Address { get; private set; }

    public string? PostalCode { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual AddressType? AddressType { get; private set; }

    public virtual Location? Location { get; private set; }

    public virtual Profile? Profile { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
