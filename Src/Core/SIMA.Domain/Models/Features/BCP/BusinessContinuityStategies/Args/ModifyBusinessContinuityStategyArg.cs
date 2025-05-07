namespace SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;

public class ModifyBusinessContinuityStategyArg
{
       public string? Title { get; set; }
    public DateTime? ExpireDate { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
