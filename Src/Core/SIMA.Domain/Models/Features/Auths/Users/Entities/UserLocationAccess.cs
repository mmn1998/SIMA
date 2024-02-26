using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.Users.Entities;

public class UserLocationAccess
{
    private UserLocationAccess() { }
    private UserLocationAccess(CreateUserLocationAccessArg arg)
    {
        Id = new UserLocationAccessId(IdHelper.GenerateUniqueId());
        if (arg.UserId.HasValue) UserId = new UserId(arg.UserId.Value);
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value); ;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = DateTime.Now;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<UserLocationAccess> Create(CreateUserLocationAccessArg arg)
    {
        return new UserLocationAccess(arg);
    }
    public void Modify(ModifyUserLocationArg arg)
    {
        UserId = new UserId(arg.Id);
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value);
        ModifiedAt = arg.ModifiedAt;
        ModifiedBy = arg.ModifiedBy;
        ActiveStatusId = arg.ActiveStatusId;
    }
    public UserLocationAccessId Id { get; private set; }

    public UserId? UserId { get; private set; }

    public LocationId? LocationId { get; private set; }

    public DateOnly? ActiveFrom { get; private set; }

    public DateOnly? ActiveTo { get; private set; }

    public long ActiveStatusId { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifiedAt { get; private set; }

    public long? ModifiedBy { get; private set; }

    public virtual Location? Location { get; private set; }

    public virtual User? User { get; private set; }
    public void Delete()
    {
        ActiveStatusId = (int)ActiveStatusEnum.Delete;
    }
}
