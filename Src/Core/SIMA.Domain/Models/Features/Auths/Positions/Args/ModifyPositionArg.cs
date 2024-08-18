namespace SIMA.Domain.Models.Features.Auths.Positions.Args;

public class ModifyPositionArg
{
    public long Id { get; set; }
    public long? DepartmentId { get; set; }
    public long ActiveStatusId { get; set; }
    public long? BranchId { get; set; }
    public long PositionLevelId { get; set; }
    public long PositionTypeId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
