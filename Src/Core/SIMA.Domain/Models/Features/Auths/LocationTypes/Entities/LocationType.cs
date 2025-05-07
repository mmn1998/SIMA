using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Args;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Interfaces;
using SIMA.Domain.Models.Features.Auths.LocationTypes.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Core.Entities;
using SIMA.Resources;
using System.Text;

namespace SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;

public class LocationType : Entity
{
    private LocationType() { }
    private LocationType(CreateLocationTypeArg arg)
    {
        Id = new LocationTypeId(IdHelper.GenerateUniqueId());
        if (arg.ParentId.HasValue) ParentId = new LocationTypeId(arg.ParentId.Value);
        Name = arg.Name;
        Code = arg.Code;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public async Task Modify(ModifyLocationTypeArg arg, ILocationTypeService service)
    {
        await ModifyGuards(arg, service);
        Name = arg.Name;
        Code = arg.Code;
        ParentId = new LocationTypeId(arg.ParentId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public static async Task<LocationType> Create(CreateLocationTypeArg arg, ILocationTypeService service)
    {
        await CreateGuards(arg, service);
        return new LocationType(arg);
    }
    public void Delete(long userId)
    {
        ModifiedBy = userId;
        ModifiedAt = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
        ActiveStatusId = (long)ActiveStatusEnum.Delete;
    }
    #region Guards
    private static async Task CreateGuards(CreateLocationTypeArg arg, ILocationTypeService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();
        arg.ActiveStatusId.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, 0)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    private async Task ModifyGuards(ModifyLocationTypeArg arg, ILocationTypeService service)
    {
        arg.NullCheck();
        arg.Name.NullCheck();
        arg.Code.NullCheck();

        if (!await service.IsCodeUnique(arg.Code, arg.Id)) throw new SimaResultException(CodeMessges._400Code,Messages.UniqueCodeError);
    }
    #endregion
    public LocationTypeId Id { get; private set; }

    public LocationTypeId? ParentId { get; private set; }

    public string? Name { get; private set; }

    public string? Code { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    private List<Location> _locations = new();

    public ICollection<Location> Locations => _locations;

    public virtual ActiveStatus? ActiveStatus { get; private set; }
    //private List<LocationType> _locationTypes = new();

    //public virtual ICollection<LocationType> InverseParent => _locationTypes;

    public virtual LocationType? Parent { get; private set; }
}
