namespace SIMA.Domain.Models.Features.Auths.ActiveStatuses.Args;

public partial class CreateActiveStatusArg
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public long ActiveStatusId { get; set; }

    public string? IsDeleted { get; set; }

    public string? HasTimeToActiveAgain { get; set; }

    public DateTime? CreatedAt { get; set; }


    public long? CreatedBy { get; set; }


}
