namespace SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;

public class CreateStepOutputParamArg
{
    public long Id { get; set; }
    public long StepId { get; set; }
    public long DataTypeId { get; set; }
    public string? IsRequired { get; set; }
    public long ActiveStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public long? ModifiedBy { get; set; }
    public byte[]? ModifiedAt { get; set; }
}
