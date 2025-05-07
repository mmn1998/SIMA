using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Args;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.ValueObjects;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.ValueObjects;
using SIMA.Framework.Common.Helper;

namespace SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;

public class AdminLocationAccess
{
    private AdminLocationAccess() { }
    private AdminLocationAccess(CreateAdminLocationAccsessArg arg)
    {
        Id = new AdminLocationAccessId(IdHelper.GenerateUniqueId());
        if (arg.UserId.HasValue) UserId = new UserId(arg.UserId.Value);
        if (arg.LocationId.HasValue) LocationId = new LocationId(arg.LocationId.Value);
        ActiveFrom = arg.ActiveFrom;
        ActiveTo = arg.ActiveTo;
        ActiveStatusId = arg.ActiveStatusId;
        CreatedAt = arg.CreatedAt;
        CreatedBy = arg.CreatedBy;
    }
    public static async Task<AdminLocationAccess> Create(CreateAdminLocationAccsessArg arg)
    {
        return new AdminLocationAccess(arg);
    }
    public AdminLocationAccessId Id { get; private set; }

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
}
