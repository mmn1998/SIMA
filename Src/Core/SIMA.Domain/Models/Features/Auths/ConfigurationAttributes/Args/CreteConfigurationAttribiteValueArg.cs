namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Args;

public class CreteConfigurationAttribiteValueArg
{
    public long Id { get; set; }

    public long ConfigurationAttributeId { get; set; }

    public string? Value { get; set; }
    public string? AttributeKey { get; set; }

    public string? IsUserConfige { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }

    public byte[]? ModifiedAt { get; set; }

    public long? ModifiedBy { get; set; }
}
