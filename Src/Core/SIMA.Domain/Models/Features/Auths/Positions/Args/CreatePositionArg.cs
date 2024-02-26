namespace SIMA.Domain.Models.Features.Auths.Positions.Args;

public class CreatePositionArg
{
    public long Id { get; set; }
    public long? DepartmentId { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long CompanyId { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }

}
