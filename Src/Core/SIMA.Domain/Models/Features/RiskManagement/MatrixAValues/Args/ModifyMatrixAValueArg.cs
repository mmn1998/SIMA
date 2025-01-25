namespace SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Args;

public class ModifyMatrixAValueArg
{
    public string? Color { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public float NumericValue { get; set; }
    public string? ValueTitle { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}