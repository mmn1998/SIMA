namespace SIMA.Domain.Models.Features.BCP.ServicePriorities.Args;

public class ModifyOrganizationalServicePriorityArg
{
    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }
    public float Ordering { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}