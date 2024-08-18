namespace SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Args;

public class CreateConfigurationAttributeArg
{
    public long Id { get; set; }
    public long ConfigurationTypeId { get; set; }

    public string? EnglishKey { get; set; }

    public string? Name { get; set; }

    public string? IsUserConfige { get; set; }

    public long ActiveStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? CreatedBy { get; set; }
}
