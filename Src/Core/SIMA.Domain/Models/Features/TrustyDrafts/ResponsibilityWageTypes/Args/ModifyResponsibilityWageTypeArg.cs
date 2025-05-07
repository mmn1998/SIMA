namespace SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Args;

public class ModifyResponsibilityWageTypeArg
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}