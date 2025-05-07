namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiMethodCalls.Args;

public class ModifyApiMethodCallArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
