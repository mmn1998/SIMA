namespace SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;

public partial class ActiveStatus
{
    public long Id { get; private set; }

    public string? Name { get; private set; }

    public string? IsActive { get; private set; }

    public string? IsDeleted { get; private set; }

    public string? HasTimeToActiveAgain { get; private set; }

    public DateTime? CreatedAt { get; private set; }

    public long? CreatedBy { get; private set; }

    public byte[]? ModifyAt { get; private set; }

    public int? ModifyBy { get; private set; }


}
