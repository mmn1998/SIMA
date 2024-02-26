using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Profiles.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;

namespace SIMA.Domain.Models.Features.Auths.Profiles.Entities;

public class Profile : Entity
{
    private Profile()
    {

    }
    private Profile(CreateProfileArg arg)
    {
        Id = new ProfileId(IdHelper.GenerateUniqueId());
        FirstName = arg.FirstName;
        LastName = arg.LastName;
        FatherName = arg.FatherName;
        if (arg.GenderId.HasValue) GenderId = new GenderId(arg.GenderId.Value);
        NationalId = arg.NationalId;
        Brithday = arg.Brithday;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }

    public static async Task<Profile> Create(IProfileService service, CreateProfileArg arg)
    {
        await CreateGuards(arg, service);
        return new Profile(arg);
    }
    #region Guards
    private static async Task CreateGuards(CreateProfileArg arg, IProfileService service)
    {
        arg.NullCheck();
        if (!service.IsValidNationalCode(arg.NationalId)) throw SimaResultException.NationalCodeIsInvalidError;
    }
    //private async Task ModifyGuards(CreateProfileArg arg, IProfileService service)
    //{
    //    arg.NullCheck();
    //    if (!service.IsValidNationalCode(arg.NationalId)) throw SimaResultException.NationalCodeIsInvalidError;
    //}
    #endregion
    public ProfileId Id { get; private set; }

    public string? FirstName { get; private set; }

    public string? LastName { get; private set; }

    public string? FatherName { get; private set; }

    public GenderId? GenderId { get; private set; }

    public string? NationalId { get; private set; }

    public DateOnly? Brithday { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    #region -- AddressBook --
    private List<AddressBook> _addressBook = new();
    public ICollection<AddressBook> AddressBooks => _addressBook;

    public async Task AddAddressBook(IProfileService profileService, CreateAddressBookArg addressBookArg)
    {
        var addressBook = await AddressBook.Create(profileService, addressBookArg);
        _addressBook.Add(addressBook);
    }

    public void RemoveAddressBook(long addressId)
    {
        var address = _addressBook.FirstOrDefault(x => x.Id == new AddressBookId(addressId));
        address.NullCheck();
        _addressBook.Remove(address);
    }

    public void ModifyAddressBook(ModifyAddressBookArg arg)
    {
        var address = _addressBook.FirstOrDefault(x => x.Id == new AddressBookId(arg.Id));
        address.NullCheck();
        address.Modify(arg);
    }
    #endregion
    #region -- PhoneBook --
    private List<PhoneBook> _phoneBooks = new();
    public ICollection<PhoneBook> PhoneBooks => _phoneBooks;

    public async Task AddPhobeBook(CreatePhoneBookArg phoneBookArg)
    {
        var phoneBook = await PhoneBook.Create(phoneBookArg);
        _phoneBooks.Add(phoneBook);
    }

    public void RemovePhoneBook(long phoneId)
    {
        var phonebook = _phoneBooks.FirstOrDefault(x => x.Id == new PhoneBookId(phoneId));
        phonebook.NullCheck();
        phonebook.Delete();
    }

    public void ModifyPhoneBook(ModifyPhoneBookArg arg)
    {
        var phone = _phoneBooks.FirstOrDefault(x => x.Id == new PhoneBookId(arg.Id));
        phone.NullCheck();
        phone.Modify(arg);
    }
    #endregion


    public virtual Gender? Gender { get; private set; }

    //ایجاد استف باید در اگریگیت خودش ایجاد بشه
    private List<Staff> _staffManager = new();
    public ICollection<Staff> StaffManagers => _staffManager;

    private List<Staff> _staffProfile = new();

    public ICollection<Staff> StaffProfiles => _staffProfile;
    // یوزر هم در اگریگیت خودش ایجاد میشه
    private List<User> _user = new();
    public ICollection<User> Users => _user;
    public void Delete()
    {
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
}
