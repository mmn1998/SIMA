using SIMA.Domain.Models.Features.AssetsAndConfigurations.Assets.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItems.Entities;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.Entities;
using SIMA.Domain.Models.Features.Auths.CompanyBuildingLocations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Args;
using SIMA.Domain.Models.Features.Auths.Locations.Interfaces;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.Locations.Entities;

public class Location : Entity
{
    private Location() { }
    private Location(CreateLocationArg arg)
    {
        Id = new LocationId(IdHelper.GenerateUniqueId());
        Name = arg.Name;
        Code = arg.Code;
        LocationTypeId = new LocationTypeId(arg.LocationTypeId.Value);
        ParentId = new LocationId(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<Location> Create(CreateLocationArg arg, ILocationService service)
    {
        await CreateGuards(arg, service);
        return new Location(arg);
    }
    public async Task Modify(ModifyLocationArg arg, ILocationService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        LocationTypeId = new LocationTypeId(arg.LocationTypeId.Value);
        ParentId = new LocationId(arg.ParentId.Value);
        ActiveStatusId = arg.ActiveStatusId;
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }

    #region Guards
    private static async Task CreateGuards(CreateLocationArg arg, ILocationService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyLocationArg arg, ILocationService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public LocationId Id { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public LocationId? ParentId { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public LocationTypeId? LocationTypeId { get; private set; }

    public ICollection<AddressBook> AddressBooks { get; set; } = new List<AddressBook>();

    public virtual ICollection<AdminLocationAccess> AdminLocationAccesses { get; set; } = new List<AdminLocationAccess>();

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    //public virtual ICollection<Location> InverseParent { get; set; } = new List<Location>();

    public virtual Location? Parent { get; set; }

    //public virtual LocationType? ParentNavigation { get; set; }
    private List<UserLocationAccess> _userLocationAccesses = new();

    public ICollection<UserLocationAccess> UserLocationAccesses => _userLocationAccesses;
    private List<Branch> _branches = new();

    public ICollection<Branch> Branches => _branches;
    private List<CompanyBuildingLocation> _companyBuildingLocations = new();

    public ICollection<CompanyBuildingLocation> CompanyBuildingLocations => _companyBuildingLocations;

    public virtual LocationType? LocationType { get; private set; }
    private List<Asset> _assets = new();
    public ICollection<Asset> Assets => _assets;
    private List<ConfigurationItem> _configurationItems = new();
    public ICollection<ConfigurationItem> ConfigurationItems => _configurationItems;
    private List<CurrencyPaymentChannel> _currencyPaymentChannels = new();
    public ICollection<CurrencyPaymentChannel> CurrencyPaymentChannels => _currencyPaymentChannels;
}
