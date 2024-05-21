namespace SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Args;

public class CreateImportanceDegreeArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }
    public float Ordering { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}