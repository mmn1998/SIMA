namespace SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Args;

public class ModifyApiAuthentoicationMethodArg
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public long ActiveStatusId { get; set; }
    public byte[]? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}
