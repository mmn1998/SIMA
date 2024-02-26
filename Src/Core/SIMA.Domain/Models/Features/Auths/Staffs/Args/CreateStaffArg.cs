namespace SIMA.Domain.Models.Features.Auths.Staffs.Args;

public class CreateStaffArg
{
    public long Id { get; set; }

    public long? ProfileId { get; set; }

    public long? ManagerId { get; set; }

    public long? PositionId { get; set; }

    public string? StaffNumber { get; set; }

    public DateOnly? ActiveFrom { get; set; }

    public DateOnly? ActiveTo { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
